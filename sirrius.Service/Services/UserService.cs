using AutoMapper;
using Microsoft.Extensions.Options;
using sirrius.Core;
using sirrius.Data.Entity;
using sirrius.Model.Entity.Token;
using sirrius.Model.Entity.User;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;
using Utilities.Helper;
using BCryptNet = BCrypt.Net.BCrypt;

namespace sirrius.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, int> userRepository;
        private readonly IRepository<Role, int> roleRepository;
        private readonly IRepository<CompanyUser, int> companyUserRepository;

        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        private readonly ApiSettings appSettings;

        public UserService(IRepository<User, int> userRepository,
                           IRepository<Role, int> roleRepository,
                           IRepository<CompanyUser, int> companyUserRepository,
                           ITokenService tokenService,
                           IOptions<ApiSettings> appSettings,
                           IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.companyUserRepository = companyUserRepository;
            this.tokenService = tokenService;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        public async Task<TokenResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            User user = null;

            if (model.Username.IsValidEmail())
                user = await userRepository.FindAsync(q => model.Username.IsValidEmail() && model.Username.Trim().ToLower().Equals(q.Email.Trim().ToLower()));
            else
                user = await userRepository.FindAsync(q => model.Username.Trim().ToLower().Equals(q.UserName.Trim().ToLower()));

            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                return null;

            user.RefreshToken = tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(appSettings.RefreshTokenExpiryTime);

            await userRepository.UpdateAsync(user);

            var response = new TokenResponse();
            response.AccessToken = tokenService.GenerateAccessToken(GetClaims(user));
            response.RefreshToken = user.RefreshToken;

            return response;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await userRepository.FindByIdAsync(id);
            await userRepository.DeleteAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await userRepository.FindAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await userRepository.FindByIdAsync(id);
            user.Role = await roleRepository.FindByIdAsync(user.RoleId);
            return user;
        }

        public async Task<User> CreateAsync(User model)
        {
            return await userRepository.InsertAsync(model);
        }

        public async Task<User> UpdateAsync(int id, User model)
        {
            var user = await userRepository.FindByIdAsync(id);

            if (model.UserName != user.UserName && userRepository.FindAll().Any(x => x.UserName == model.UserName))
                throw new AppException("Username '" + model.UserName + "' is already taken");

            // hash password if it was entered
            //if (!string.IsNullOrEmpty(model.PasswordHash))
            //    user.PasswordHash = BCryptNet.HashPassword(model.PasswordHash);

            // copy model to user and save
            //mapper.Map(model, user);

            user.UpdatedBy = user.Id;
            user.UpdatedAt = DateTime.UtcNow;

            return await userRepository.UpdateAsync(user);
        }

        public async Task<string> GetNewTokenAsync(string refreshToken)
        {
            var users = await userRepository.FindAllAsync();
            var user = users.FirstOrDefault(q => q.RefreshToken == refreshToken);

            if (user == null)
                return null;

            if (user.RefreshTokenExpiryTime.AddDays(appSettings.RefreshTokenExpiryTime) < DateTime.Now)
                return null;

            //var response = new TokenResponse();
            //response.AccessToken = tokenService.GenerateAccessToken(GetClaims(user));
            //response.RefreshToken = user.RefreshToken;
            //return response;

            return tokenService.GenerateAccessToken(GetClaims(user));
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var role = roleRepository.FindById(user.RoleId);
            claims.Add(new Claim("role", role.Name));

            return claims;
        }

        public async Task<(bool exist, string message)> IsExist(string username)
        {
            string message = string.Empty;

            var users = await GetAllAsync();

            return (users.Any(q => SirriusHelper.fullTextSearch(q.UserName.ToLower(), username.ToLower())), "Username already exist");
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await roleRepository.FindAllAsync();
        }

        public async Task<IEnumerable<CompanyUser>> GetAllCompanyUserAsync(IEnumerable<int> companyIds)
        {
            return await companyUserRepository.FindAllAsync(q => companyIds.Contains(q.CompanyId));
        }
    }
}
