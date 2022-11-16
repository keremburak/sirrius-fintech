using CookieManager;
using sirrius.Data.Entity;
using sirrius.Model;
using sirrius.Model.DataModel;
using sirrius.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using Utilities.Helper;
using sirrius.WebUI.Controllers;

namespace sirrius.Web.Controllers
{
    [Authorize("SuperAdmin,Admin")]
    public class CategoryController : BaseController
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ICookie cookieService, IOptions<AppSettings> options)
            : base(cookieService, options)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/category/list")]
        public async Task<JsonResult> GetCategories(Parameters parameters)
        {
            //var qs = !string.IsNullOrEmpty(parameters.querystring) ? JsonConvert.DeserializeObject<string[]>(parameters.querystring) : null;

            var result = new AjaxResultModel();
            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.POST, parameters, Settings.ApiBaseURL + "/api/v1.0/category/list", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = StatusCodes.Status500InternalServerError;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "liste alınırken hata oluştu. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<GridResultModel<Category>>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";

            return Json(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = new Category();

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/category/" + id.ToString(), CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));


            if (apiResponse == null || apiResponse.IsError)
            {
                _logger.LogError("İşlem  bilgisi alinirken hata olustu");

                return View(category);
            }

            category = JsonConvert.DeserializeObject<Category>(apiResponse.Data.ToString());

            return View(category);
        }

        #region create category       

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/category/create")]
        public async Task<JsonResult> CreateCategory()
        {
            var category = new Category
            {
                //Code = Request.Form["Code"].ToString(),
                Name = Request.Form["Name"].ToString(),
                // Description = Request.Form["Desc"].ToString(),
                CreatedBy = CookieHelper.GetUser(HttpContext).Id,
                CreatedAt = DateTime.Now
            };

            var result = new AjaxResultModel();

            if (!TryValidateModel(category))
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "Hatalı model";

                return Json(result);
            }

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.POST, category, Settings.ApiBaseURL + "/api/v1.0/category/create", CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

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
                    result.message = "Kategori adı onceden girilmis. Lutfen baska kategori adı deneyin.";
                else
                    result.message = "Kayıt olusturulurken hata ile karsilasildi. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<int>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";
            result.isRedirectRequired = true;
            result.redirectUrl = Url.Action("Index", "Category");

            return Json(result);
        }

        #endregion


        #region edit category

        public async Task<IActionResult> Edit(int id)
        {
            var category = new Category();

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/category/" + id.ToString(), CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));


            if (apiResponse == null || apiResponse.IsError)
            {
                _logger.LogError("Kategori bilgisi alınırken hata olustu");

                return View(category);
            }

            category = JsonConvert.DeserializeObject<Category>(apiResponse.Data.ToString());

            return View(category);
        }

        [HttpPost("/category/edit")]
        public async Task<JsonResult> EditCategory()
        {
            var result = new AjaxResultModel();

            if (!int.TryParse(Request.Form["Id"].ToString(), out var id))
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "İşlem kaydi guncellenirken hata ile karsilasildi. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            var category = new Category
            {
                Id = id,
                //Code = Request.Form["Code"].ToString(),
                Name = Request.Form["Name"].ToString(),
                // Description = Request.Form["Desc"].ToString(),
            };

            if (!TryValidateModel(category))
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "Hatalı model";

                return Json(result);
            }

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.PUT, category, Settings.ApiBaseURL + "/api/v1.0/category/" + id, CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "İşlem  kaydı guncellenirken hata ile karşılaşıldı. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<Model.DataModel.NoContentResult>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";
            result.isRedirectRequired = true;
            result.redirectUrl = Url.Action("Index", "Category");

            return Json(result);
        }

        #endregion


        #region delete category

        public async Task<IActionResult> Delete(int id)
        {
            var category = new Category();

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.GET, null, Settings.ApiBaseURL + "/api/v1.0/category/" + id, CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null || apiResponse.IsError)
            {
                _logger.LogError("İşlem  bilgisi alinirken hata olustu");

                return View(category);
            }

            category = JsonConvert.DeserializeObject<Category>(apiResponse.Data.ToString());

            return View(category);
        }


        [HttpPost("/category/delete")]
        public async Task<JsonResult> DeleteCategory([FromBody] Category category)
        {
            var result = new AjaxResultModel();

            if (category == null)
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "İşlem kaydi silinirken hata ile karşılaşıldı. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            if (!TryValidateModel(category))
            {
                result.httpStatusCode = (int)HttpStatusCode.BadRequest;
                result.message = "Hatalı model";

                return Json(result);
            }

            var apiResponse = await RestClientHelper.PostMethodAsync<ApiResponse>(Method.DELETE, null, Settings.ApiBaseURL + "/api/v1.0/category/" + category.Id, CookieHelper.GetTokenHeader(HttpContext, "crmtoken"));

            if (apiResponse == null)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "Servis hatası.";

                return Json(result);
            }

            if (apiResponse.IsError)
            {
                result.httpStatusCode = apiResponse.HttpStatusCode;
                result.message = "İşlem kaydı silinirken hata ile karşılaşıldı. Lütfen daha sonra tekrar deneyin.";

                return Json(result);
            }

            result.done = !apiResponse.IsError;
            result.data = JsonConvert.DeserializeObject<Model.DataModel.NoContentResult>(apiResponse.Data.ToString());
            result.httpStatusCode = apiResponse.HttpStatusCode;
            result.message = "İşlem başarılı";
            result.isRedirectRequired = true;
            result.redirectUrl = Url.Action("Index", "Category");

            return Json(result);
        }

        #endregion

    }
}
