using Microsoft.AspNetCore.Mvc;
using sirrius.Model.Entity.Token;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirrius.CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : BaseController
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public TokenController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenResponse tokenResponse)
        {
            if (tokenResponse == null)
                return BadRequest("Invalid Client Request");

            var principal = tokenService.GetPrincipalFromExpiredToken(tokenResponse.AccessToken);
            //var username = principal.Identity.Name;
            var users = await userService.GetAllAsync();
            var user = users.SingleOrDefault(q => q.UserName.Trim().ToLower().Equals(principal.Identity.Name.Trim().ToLower()));

            if (user == null || user.RefreshToken != tokenResponse.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid Client Request");

            tokenResponse.AccessToken = tokenService.GenerateAccessToken(principal.Claims);
            tokenResponse.RefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = tokenResponse.RefreshToken;
            await userService.UpdateAsync(user.Id, user);

            return Ok(tokenResponse);
        }


        [HttpPost, Authorize("SuperAdmin")]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var users = await userService.GetAllAsync();
            var user = users.SingleOrDefault(u => u.Id == MyUser.Id);

            if (user == null) return BadRequest("Invalid Client Request");

            user.RefreshToken = null;
            await userService.UpdateAsync(user.Id, user);

            return Ok();
        }

        //[HttpPost, Authorize("SuperAdmin")]
        //[Route("revoke-all")]
        //public IActionResult RevokeAll()
        //{
        //    var user = userService.GetAll().SingleOrDefault(u => u.Id == CRMUser.Id);

        //    if (user == null) return BadRequest("Invalid Client Request");

        //    user.RefreshToken = null;
        //    userService.Update(user.Id, user);

        //    return NoContent();
        //}
    }
}
