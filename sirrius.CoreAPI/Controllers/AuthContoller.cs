using Microsoft.AspNetCore.Mvc;
using sirrius.CoreAPI.Authorization;
using sirrius.Model.Entity.Token;
using sirrius.Model.Entity.User;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirrius.CoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService userService;
        private readonly ILogService logService;

        public AuthController(IUserService userService, ILogService logService)
        {
            this.userService = userService;
            this.logService = logService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<TokenResponse>> AuthenticateAsync([FromBody] AuthenticateRequest model)
        {
            var response = await userService.AuthenticateAsync(model);

            if (response == null)
            {
                logService.LogError("Token not generated");

                return Unauthorized();
            }

            //logService.LogInfo($"Token generated. Token : {response.AccessToken}");

            return Ok(response);
        }

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //public ActionResult<TokenResponse> Authenticate([FromBody] AuthenticateRequest model)
        //{
        //    var response = userService.Authenticate(model);

        //    if (response == null)
        //        return Unauthorized();

        //    Response.Cookies.Append("X-Access-Token", response.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Lax });
        //    Response.Cookies.Append("X-Username", model.Username, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Lax });
        //    Response.Cookies.Append("X-Refresh-Token", response.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Lax});


        //    return Ok();
        //}

        [AllowAnonymous]
        [HttpPost("new-token")]
        public async Task<ActionResult<string>> GetNewToken([FromBody] string refreshToken)
        {
            var response = await userService.GetNewTokenAsync(refreshToken);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }
    }
}
