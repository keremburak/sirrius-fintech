using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using sirrius.Core;
using sirrius.Data.Entity;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Helper;

namespace sirrius.CoreAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ApiSettings appSettings;
        private readonly IRepository<User, int> userRepository;
        private readonly IRepository<Role, int> roleRepository;

        public JwtMiddleware(IRepository<User, int> userRepository,
                             IRepository<Role, int> roleRepository,
                             RequestDelegate next,
                             IOptions<ApiSettings> appSettings)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;

            this.next = next;
            this.appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, ITokenService tokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = tokenService.ValidateAccessToken(token);

            if (userId != null)
            {
                // attach account to context on successful jwt validation
                var user = await userRepository.FindByIdAsync(userId.Value);
                user.Role = await roleRepository.FindByIdAsync(user.RoleId);

                context.Items["User"] = user;
            }

            await next(context);
        }
    }
}
