using CookieManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Utilities.Helper;

namespace sirrius.WebUI.Controllers
{
    [Authorize("SuperAdmin,Admin,User")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        public HomeController(ILogger<HomeController> logger, ICookie cookieService, IOptions<Models.AppSettings> options)
          : base(cookieService, options)
        {
            //_httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["user"];

            return View();
        }

        #region dashboard 

        [Authorize("Admin,User")]
        public IActionResult Dashboard()
        {
            return View(EnumHelper.GetEnumValueFromString<EnumHelper.Roles>(CookieHelper.GetRole(HttpContext)));
        }

        //dashboard daily graph on top
        [HttpPost("/dashboard/dailydata")]
        public async Task<JsonResult> GetDashboardDailyData([FromBody] DailyChartParameters parameters)
        {
            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.POST, parameters, Settings.ApiBaseURL + "/api/v1.0/dashboard/dailydata", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = StatusCodes.Status500InternalServerError;
                result.message = "Servis hatası. Daha sonra tekrar deneyin.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Genel durum verisi alınırken hata oluştu. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<Dictionary<string, List<DailyGeneralItem>>>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";

            return Json(result);
        }

        //dashboard doughnut chart
        [HttpGet("/dashboard/amountdata")]
        public async Task<JsonResult> GetDashboardAmountData([FromQuery] string cid)
        {
            Parameters parameters = new Parameters();

            if (!string.IsNullOrEmpty(cid))
                parameters.querystring = "cid=" + cid;

            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.POST, parameters, Settings.ApiBaseURL + "/api/v1.0/dashboard/amountdata", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = StatusCodes.Status500InternalServerError;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Bakiyeler alınırken hata oluştu. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<DashboardExtraViewModel>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";

            return Json(result);
        }

        #endregion

        #region dashboard detail

        [Authorize("Admin,User")]
        public IActionResult DashboardDetail()
        {
            return View(EnumHelper.GetEnumValueFromString<EnumHelper.Roles>(CookieHelper.GetRole(HttpContext)));
        }

        //dashboard detail transaction table data
        [HttpGet("/dashboard/bstdata")] //bank statement transaction data
        public async Task<JsonResult> GetBankStatementTransactions(Parameters parameters, [FromQuery] string cid = "", [FromQuery] string cba = "", [FromQuery] string daterange = "")
        {
            //var qs = !string.IsNullOrEmpty(parameters.querystring) ? JsonConvert.DeserializeObject(parameters.querystring) : null;

            parameters.querystring = "cid=" + cid + "&cba=" + cba + "&daterange=" + daterange;

            //if (!string.IsNullOrEmpty(cid))
            //    parameters.querystring = "cid=" + cid;

            //if (!string.IsNullOrEmpty(cba))
            //    parameters.querystring += "&cba=" + cba;

            //if (!string.IsNullOrEmpty(daterange))
            //    parameters.querystring += "&daterange=" + daterange;

            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.POST, parameters, Settings.ApiBaseURL + "/api/v1.0/dashboard/bstdata", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = StatusCodes.Status500InternalServerError;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Banka hareketleri listesi alınırken hata oluştu. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<GridResultModel<BankStatementTransaction>>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";

            return Json(result);
        }

        #endregion

        #region common data

        [HttpGet("/dashboard/company")]
        public async Task<JsonResult> GetCompanies()
        {
            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/dashboard/companylist", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

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

        [HttpGet("/dashboard/bank")]
        public async Task<JsonResult> GetBanks()
        {
            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/dashboard/banklist", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

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

            return Json(result);
        }

        [HttpGet("/dashboard/bankaccount")]
        public async Task<JsonResult> GetBankAccounts(int bid)
        {
            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/dashboard/bankaccount/" + bid, CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = StatusCodes.Status500InternalServerError;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Hesap listesi alınırken hata oluştu. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<CompanyBankAccount>>>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";

            return Json(result);
        }

        #endregion

        [HttpGet("/dashboard/currency")]
        public async Task<JsonResult> GetCurrencies()
        {
            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/dashboard/currencylist", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = StatusCodes.Status500InternalServerError;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Para birimi listesi alınırken hata oluştu. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<GridResultModel<Data.Entity.Currency>>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";

            return Json(result);
        }

        [AllowAnonymous]
        [HttpGet("/dashboard/rates")]
        public JsonResult GetExchangeRates()
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();

            try
            {
                XmlDocument xml = new XmlDocument(); // yeni bir XML dökümü oluşturuyoruz.
                xml.Load("http://www.tcmb.gov.tr/kurlar/today.xml"); // bağlantı kuruyoruz.

                var date_nodes = xml.SelectSingleNode("//Tarih_Date"); // Count değerini olmak için ana boğumu seçiyoruz.
                var CurrencyNodes = date_nodes.SelectNodes("//Currency"); // ana boğum altındaki kur boğumunu seçiyoruz.
                int CurrencyLength = CurrencyNodes.Count; // toplam kur boğumu sayısını elde ediyor ve for döngüsünde kullanıyoruz.

                for (int i = 0; i < CurrencyLength; i++) // for u çalıştırıyoruz.
                {
                    var cn = CurrencyNodes[i]; // kur boğumunu alıyoruz.
                                               // Listeye kur bilgirini ekliyoruz.
                    exchangeRates.Add(new ExchangeRate
                    {
                        Kod = cn.Attributes["Kod"].Value,
                        CrossOrder = cn.Attributes["CrossOrder"].Value,
                        CurrencyCode = cn.Attributes["CurrencyCode"].Value,
                        Unit = cn.ChildNodes[0].InnerXml,
                        Isim = cn.ChildNodes[1].InnerXml,
                        CurrencyName = cn.ChildNodes[2].InnerXml,
                        ForexBuying = cn.ChildNodes[3].InnerXml,
                        ForexSelling = cn.ChildNodes[4].InnerXml,
                        BanknoteBuying = cn.ChildNodes[5].InnerXml,
                        BanknoteSelling = cn.ChildNodes[6].InnerXml,
                        CrossRateOther = cn.ChildNodes[8].InnerXml,
                        CrossRateUSD = cn.ChildNodes[7].InnerXml,
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Json(exchangeRates);
        }

        public IActionResult ProfileSettings()
        {
            return View();
        }
    }
}
