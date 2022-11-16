using CookieManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Helper;
using sirrius.Model;
using Utilities.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly IList<string> _roles;
    //private User user = new User();

    public AuthorizeAttribute(string roles)
    {
        _roles = !string.IsNullOrEmpty(roles) ? roles.Split(",") : new string[] { };
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        //_settings = context.HttpContext.RequestServices.GetService<IOptions<AppSettings>>().Value;


        //var user = SessionHelper.Get<TokenResponse>(context.HttpContext.Session, "user");

        var _configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

        //var token = context.HttpContext.Request.Cookies["crmtoken"].Decrypt(_configuration.GetSection("AppSettings:ClientSecret").Value);
        var token = CookieHelper.GetToken(context.HttpContext, "crmtoken");

        if (!string.IsNullOrEmpty(token))
        {
            //var user = CookieHelper.GetUser(Encoding.UTF8.GetString(Convert.FromBase64String(token)));
            var user = CookieHelper.GetUser(token);

            if (user.Id == 0 || (_roles.Any() && !_roles.Contains(user.RoleName)))
            {
                // not logged in or role not authorized
                //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                //context.Result = new RedirectResult("~/Home/Unauthorized401");

                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Unauthorized401" }));
            }
        }
        else
        {
            token = CookieHelper.GetToken(context.HttpContext, "crmreftoken"); //refresh token

            //if token is expired & there is a refresh token, get a new token
            //if (context.HttpContext.Request.Cookies["crmreftoken"] != null)
            if (!string.IsNullOrEmpty(token))
            {
                var _cookieService = context.HttpContext.RequestServices.GetService<ICookie>();

                //var tokenValue = context.HttpContext.Request.Cookies["crmreftoken"].Decrypt((_configuration.GetSection("AppSettings:ClientSecret").Value));
                var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(RestSharp.Method.POST, token, _configuration.GetSection("AppSettings:ApiBaseURL").Value + "/api/auth/new-token");

                if (apiResponse != null && !apiResponse.IsError)
                {
                    var accessToken = apiResponse.Data.ToString();

                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        _cookieService.Set("crmtoken", accessToken.Encrypt(_configuration.GetSection("AppSettings:ClientSecret").Value), new CookieOptions { Expires = DateTime.Now.AddMinutes(1) });
                        //_cookieService.Set("crmreftoken", tokenResponse.RefreshToken, new CookieOptions { Expires = DateTime.Now.AddDays(7) });
                        context.Result = new RedirectToActionResult(context.RouteData.Values["action"].ToString(), context.RouteData.Values["controller"].ToString(), null);

                    }
                    else context.Result = new RedirectResult("~/Login/Index");
                }
                else
                {
                    _cookieService.Remove("crmreftoken");

                    context.Result = new RedirectResult("~/Login/Index");
                }

                //using (var client = new HttpClient())
                //{
                //    var tokenValue = Encoding.UTF8.GetString(Convert.FromBase64String(context.HttpContext.Request.Cookies["crmreftoken"]));

                //    var response = client.PostAsync(_configuration.GetSection("AppSettings:ApiBaseURL").Value + "/api/auth/new-token", new JsonContent(tokenValue)).GetAwaiter().GetResult(); ;

                //    if (response.IsSuccessStatusCode)
                //    {
                //        var responseContent = response.Content;
                //        var result = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();

                //        apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
                //    }
                //}

                //if (apiResponse != null)
                //{
                //    var accessToken = apiResponse.Data.ToString();

                //    if (!string.IsNullOrEmpty(accessToken))
                //    {
                //        var encryptedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(accessToken));

                //        _cookieService.Set("crmtoken", encryptedToken, new CookieOptions { Expires = DateTime.Now.AddMinutes(1) });
                //        //_cookieService.Set("crmreftoken", tokenResponse.RefreshToken, new CookieOptions { Expires = DateTime.Now.AddDays(7) });
                //        context.Result = new RedirectToActionResult(context.RouteData.Values["action"].ToString(), context.RouteData.Values["controller"].ToString(), null);

                //    }
                //    else context.Result = new RedirectResult("~/Login/Index");
                //}
            }
            else context.Result = new RedirectResult("~/Login/Index");
        }

    }
}