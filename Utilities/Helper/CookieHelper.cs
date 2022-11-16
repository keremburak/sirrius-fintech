using CookieManager;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace Utilities.Helper
{
    public static class CookieHelper
    {
        public static User GetUser(HttpContext context)
        {
            //var response = _httpContextAccessor.HttpContext.Request.Cookies["crmtoken"];
            //var base64EncodedBytes = Convert.FromBase64String(response);
            //var user = JsonConvert.DeserializeObject<TokenResponse>(Encoding.UTF8.GetString(base64EncodedBytes));

            //var handler = new JwtSecurityTokenHandler();
            //var jwtSecurityToken = handler.ReadJwtToken(user.AccessToken);


            User user = null;
            var cookie = context.Request.Cookies["crmtoken"];

            //var _cookieService = context.RequestServices.GetService(typeof(ICookie)) as ICookie;

            if (!string.IsNullOrEmpty(cookie))
            {
                //var base64EncodedBytes = Convert.FromBase64String(cookie);
                var _configuration = context.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

                var tokenHandler = new JwtSecurityTokenHandler();
                //var accessToken = tokenHandler.ReadJwtToken(Encoding.UTF8.GetString(Convert.FromBase64String(cookie)));
                var accessToken = tokenHandler.ReadJwtToken(cookie.ToString().Decrypt(_configuration.GetSection("AppSettings:ClientSecret").Value));

                user = new User();
                user.Id = int.Parse(accessToken.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value);
                user.UserName = accessToken.Claims.First(q => q.Type == ClaimTypes.Name).Value;
                user.FullName = accessToken.Claims.First(q => q.Type == ClaimTypes.GivenName).Value;
                user.RoleName = accessToken.Claims.First(q => q.Type == "role").Value;
                user.Email = accessToken.Claims.First(q => q.Type == ClaimTypes.Email).Value;
            }

            return user;
        }

        public static User GetUser(string token)
        {
            //var response = _httpContextAccessor.HttpContext.Request.Cookies["crmtoken"];
            //var base64EncodedBytes = Convert.FromBase64String(response);
            //var user = JsonConvert.DeserializeObject<TokenResponse>(Encoding.UTF8.GetString(base64EncodedBytes));

            //var handler = new JwtSecurityTokenHandler();
            //var jwtSecurityToken = handler.ReadJwtToken(user.AccessToken);



            var user = new User();

            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.ReadJwtToken(token);
            user.Id = int.Parse(accessToken.Claims.First(q => q.Type == ClaimTypes.NameIdentifier).Value);
            user.UserName = accessToken.Claims.First(q => q.Type == ClaimTypes.Name).Value;
            user.FullName = accessToken.Claims.First(q => q.Type == ClaimTypes.GivenName).Value;
            user.RoleName = accessToken.Claims.First(q => q.Type == "role").Value;
            user.Email = accessToken.Claims.First(q => q.Type == ClaimTypes.Email).Value;

            return user;
        }

        public static string GetRole(HttpContext context)
        {
            string role = string.Empty;

            var cookie = context.Request.Cookies["crmtoken"];

            //var _cookieService = context.RequestServices.GetService(typeof(ICookie)) as ICookie;

            if (!string.IsNullOrEmpty(cookie))
            {
                //var base64EncodedBytes = Convert.FromBase64String(cookie);
                var _configuration = context.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

                var tokenHandler = new JwtSecurityTokenHandler();
                //var accessToken = tokenHandler.ReadJwtToken(Encoding.UTF8.GetString(Convert.FromBase64String(cookie)));
                var accessToken = tokenHandler.ReadJwtToken(cookie.ToString().Decrypt(_configuration.GetSection("AppSettings:ClientSecret").Value));

                role = accessToken.Claims.First(q => q.Type == "role").Value;
            }

            return role;
        }

        public static string GetToken(HttpContext context, string key)
        {
            var _configuration = context.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var _cookieService = context.RequestServices.GetService(typeof(ICookie)) as ICookie;

            return _cookieService.Get(key).Decrypt(_configuration.GetSection("AppSettings:ClientSecret").Value);
        }

        public static Dictionary<string, string> GetTokenHeader(HttpContext context, string key)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("Authorization", $"Bearer {GetToken(context, key)}");

            return tokenHeader;
        }
    }
}
