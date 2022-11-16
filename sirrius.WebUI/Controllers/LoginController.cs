using CookieManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using sirrius.Model;
using sirrius.Model.Entity.Token;
using sirrius.Model.Entity.User;
using sirrius.Service.Interfaces;
using sirrius.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utilities.Extensions;
using Utilities.Helper;

namespace sirrius.WebUI.Controllers
{
    public class LoginController : BaseController
    {
        //HttpClient CRMclient;

        //public LoginController(IHttpClientFactory httpClientFactory)
        //{
        //    CRMclient = httpClientFactory.CreateClient("sirrius-api");
        //}

        private readonly ILogger<LoginController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ICookie _cookieService;
        private readonly AppSettings _settings;

        public LoginController(ILogger<LoginController> logger,
                               IHttpContextAccessor httpContextAccessor,
                               ICookie cookieService,
                               IOptions<AppSettings> options)
                               //IHttpClientFactory httpClientFactory)
            : base(cookieService, options)
        {
            _httpContextAccessor = httpContextAccessor;
            //_cookieService = cookieService;
            _settings = options.Value;
            _logger = logger;

            //CRMclient = httpClientFactory.CreateClient("sirrius-api");
        }

        public async Task<IActionResult> Index()
        {
            //if (_httpContextAccessor.HttpContext.Request.Cookies["crmtoken"] != null)
            if (!string.IsNullOrEmpty(CookieService.Get("crmtoken")))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //if (_httpContextAccessor.HttpContext.Request.Cookies["crmreftoken"] != null)
                if (!string.IsNullOrEmpty(CookieService.Get("crmreftoken")))
                {
                    //var tokenValue = _httpContextAccessor.HttpContext.Request.Cookies["crmreftoken"].ToString().Decrypt(_settings.ClientSecret);
                    //var apiResponse = RestClientHelper.PostMethod<ApiResponse>(CookieService.Get("crmreftoken").Decrypt(_settings), _settings.ApiBaseURL + "/api/auth/new-token");
                    var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.POST, CookieHelper.GetToken(HttpContext, "crmreftoken"), Settings.ApiBaseURL + "/api/auth/new-token");

                    if (apiResponse != null && !apiResponse.IsError)
                    {
                        var accessToken = apiResponse.Data.ToString();

                        if (accessToken != null)
                        {
                            CookieService.Set("crmtoken", accessToken.Encrypt(Settings.ClientSecret), new CookieOptions { Expires = DateTime.Now.AddMinutes(30) });

                            //_cookieService.Set("crmtoken", tokenResponse.AccessToken, new CookieOptions { Expires = DateTime.Now.AddMinutes(30) });
                            //_cookieService.Set("crmreftoken", tokenResponse.RefreshToken, new CookieOptions { Expires = DateTime.Now.AddDays(7) });

                            return RedirectToAction("Index", "Home");
                        }
                        else return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        CookieService.Remove("crmreftoken");

                        return RedirectToAction("Index", "Login");
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Index([FromBody] AuthenticateRequest loginModel)
        {
            TokenResponse tokenResponse = null;
            var result = new AjaxResultModel();

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.POST, loginModel, Settings.ApiBaseURL + "/api/auth/authenticate");

            if (apiResponse == null)
            {
                result.done = false;
                result.httpStatusCode = StatusCodes.Status500InternalServerError;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.done = false;
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Kullanici bilgilerinde hata var. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            //var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(apiResponse.Data.ToString());
            tokenResponse = RestClientHelper.GetResult<TokenResponse>(apiResponse.Data.ToString());

            if (tokenResponse != null)
            {

                CookieService.Set("crmtoken", tokenResponse.AccessToken.Encrypt(Settings.ClientSecret), new CookieOptions { Expires = DateTime.Now.AddMinutes(30) });
                CookieService.Set("crmreftoken", tokenResponse.RefreshToken.Encrypt(Settings.ClientSecret), new CookieOptions { Expires = DateTime.Now.AddDays(Settings.RefreshTokenExpiryTime) });



                //return RedirectToAction("Index", "Home");
            }


            result.done = !apiResponse.IsError;
            result.data = CookieHelper.GetUser(tokenResponse.AccessToken);
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";

            return Json(result);
        }

        public IActionResult Logout()
        {
            //if needed revoke refresh token (set NULL in Users table, uncomment below region part)
            #region Revoke Refresh Token

            //var referrer = Request.Headers["Referer"].ToString();
            //var host = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host;
            //var parts = referrer.Substring(host.Length).Split("/");

            //var apiResponse = RestClientHelper.PostMethod<ApiResponse>(Method.POST, null, Settings.ApiBaseURL + "/api/token/revoke", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            //if (apiResponse == null || apiResponse.IsError)
            //{
            //    if (parts.Length > 2)
            //        return RedirectToAction(parts[2], parts[1]);
            //    else
            //        return RedirectToAction("Index", parts[1]);
            //}

            #endregion

            CookieService.Remove("crmtoken");
            CookieService.Remove("crmreftoken");

            //_httpContextAccessor.HttpContext.Response.Cookies.Delete("crmtoken");
            //_httpContextAccessor.HttpContext.Response.Cookies.Delete("crmreftoken");

            return RedirectToAction("Index", "Login");
        }
    }
}
