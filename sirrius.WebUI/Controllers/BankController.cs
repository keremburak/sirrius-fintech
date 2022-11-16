using CookieManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using sirrius.Data.Entity;
using sirrius.Model;
using sirrius.Model.DataModel;
using sirrius.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utilities.Helper;

namespace sirrius.WebUI.Controllers
{
    [Authorize("SuperAdmin,Admin")]
    public class BankController : BaseController
    {
        private readonly ILogger<BankController> _logger;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public BankController(ILogger<BankController> logger, ICookie cookieService, IOptions<AppSettings> options)
            : base(cookieService, options)
        {
            _logger = logger;
            //_httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/bank/list")]
        public async Task<JsonResult> GetBanks(Parameters parameters)
        {
            //var qs = !string.IsNullOrEmpty(parameters.querystring) ? JsonConvert.DeserializeObject<string[]>(parameters.querystring) : null;

            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.POST, parameters, Settings.ApiBaseURL + "/api/v1.0/bank/list", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = StatusCodes.Status500InternalServerError;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Banka listesi alınırken hata oluştu. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<GridResultModel<Bank>>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";

            //ApiResponse apiResponse = null;

            //using (var client = new HttpClient())
            //{
            //    //var tokenValue = Encoding.UTF8.GetString(Convert.FromBase64String(_httpContextAccessor.HttpContext.Request.Cookies["crmreftoken"]));

            //    client.DefaultRequestHeaders.Clear();
            //    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", CookieHelper.GetToken(HttpContext, "crmtoken")));

            //    var response = client.GetAsync(Settings.ApiBaseURL + "/api/bank/list").GetAwaiter().GetResult();

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var responseContent = response.Content;
            //        var result = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();

            //        apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
            //    }
            //}

            //return Json(RestClientHelper.GetResult<IEnumerable<Bank>>(response.Data.ToString()));

            return Json(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var bank = new Bank();

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/bank/" + id.ToString(), CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));


            if (apiResponse == null || apiResponse.IsError)
            {
                _logger.LogError("Banka bilgisi alinirken hata olustu");

                return View(bank);
            }

            bank = JsonConvert.DeserializeObject<Bank>(apiResponse.Data.ToString());

            return View(bank);
        }


        #region create bank       

        public async Task<IActionResult> Create()
        {
            var bank = new Bank();

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/bank/entities", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null || apiResponse.IsError)
            {
                _logger.LogError("Şehir bilgisi alinirken hata olustu");

                return View(bank);
            }

            var model = JsonConvert.DeserializeObject<MultiSelectModel<Bank, string>>(apiResponse.Data.ToString());

            ViewBag.Countries = model.Items["country"] as IEnumerable<SelectListItem>;

            return View(model);
        }

        [HttpPost("/bank/create")]
        public async Task<JsonResult> CreateBank()
        {
            var bank = new Bank
            {
                Code = Request.Form["Code"].ToString(),
                Name = Request.Form["Name"].ToString(),
                CountryId = int.Parse(Request.Form["CountryId"].ToString()),
                Description = Request.Form["Desc"].ToString(),
                CreatedBy = CookieHelper.GetUser(HttpContext).Id,
                CreatedAt = DateTime.Now
            };

            var result = new AjaxResultModel();

            if (!TryValidateModel(bank))
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "Hatalı model";

                return Json(result);
            }

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.POST, bank, Settings.ApiBaseURL + "/api/v1.0/bank/create", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;

                if (apiResponse.HttpStatusCode == (int)StatusCodes.Status409Conflict)
                    result.message = "Banka kodu veya banka adi onceden girilmis. Lutfen baska banka kodu veya adi deneyin.";
                else
                    result.message = "Banka kaydi olusturulurken hata ile karsilasildi. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<int>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";
            result.isRedirectRequired = true;
            result.redirectUrl = Url.Action("Index", "Bank");

            return Json(result);
        }

        #endregion

        #region edit bank

        public async Task<IActionResult> Edit(int id)
        {
            MultiSelectModel<Bank, string> model = new MultiSelectModel<Bank, string>();

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/bank/entities/" + id.ToString(), CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null || apiResponse.IsError)
            {
                _logger.LogError("Banka adı alınırken hata oluştu");

                return View(model);
            }


            model = JsonConvert.DeserializeObject<MultiSelectModel<Bank, string>>(apiResponse.Data.ToString());

            ViewBag.Countries = model.Items["country"] as IEnumerable<SelectListItem>;

            return View(model);
        }

        [HttpPost("/bank/edit")]
        public async Task<JsonResult> EditBank()
        {
            var result = new AjaxResultModel();

            if (!int.TryParse(Request.Form["Id"].ToString(), out var id))
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "Banka kaydi guncellenirken hata ile karsilasildi. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            var bank = new Bank
            {
                Id = id,
                Code = Request.Form["Code"].ToString(),
                Name = Request.Form["Name"].ToString(),
                CountryId = int.Parse(Request.Form["CountryId"].ToString()),
                Description = Request.Form["Desc"].ToString(),
            };

            if (!TryValidateModel(bank))
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "Hatalı model";

                return Json(result);
            }

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.PUT, bank, Settings.ApiBaseURL + "/api/v1.0/bank/" + id, CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Banka kaydi guncellenirken hata ile karsilasildi. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<Model.DataModel.NoContentResult>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";
            result.isRedirectRequired = true;
            result.redirectUrl = Url.Action("Index", "Bank");

            return Json(result);
        }

        #endregion

        #region delete bank

        public async Task<IActionResult> Delete(int id)
        {
            var bank = new Bank();

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/bank/" + id, CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null || apiResponse.IsError)
            {
                _logger.LogError("Banka bilgisi alinirken hata olustu");

                return View(bank);
            }

            bank = JsonConvert.DeserializeObject<Bank>(apiResponse.Data.ToString());

            return View(bank);
        }


        [HttpPost("/bank/delete")]
        public async Task<JsonResult> DeleteBank([FromBody] Bank bank)
        {
            var result = new AjaxResultModel();

            if (bank == null)
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "Banka kaydi silinirken hata ile karsilasildi. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            if (!TryValidateModel(bank))
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "Hatalı model";

                return Json(result);
            }

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.DELETE, null, Settings.ApiBaseURL + "/api/v1.0/bank/" + bank.Id, CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Banka kaydi silinirken hata ile karsilasildi. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<Model.DataModel.NoContentResult>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";
            result.isRedirectRequired = true;
            result.redirectUrl = Url.Action("Index", "Bank");

            return Json(result);
        }

        #endregion 


        [HttpGet("/bank/getbypasscodes")]
        public async Task<JsonResult> GetByPassCodes()
        {
            int cid = 1;

            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/ftpconnection/companylist/" + cid, CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = StatusCodes.Status500InternalServerError;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Firma listesi alınırken hata oluştu. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<GridResultModel<Company>>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";

            return Json(result);
        }


    }
}
