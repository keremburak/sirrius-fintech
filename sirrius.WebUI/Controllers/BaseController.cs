using CookieManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using sirrius.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirrius.WebUI.Controllers
{
    public class BaseController : Controller
    {
        //public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly ICookie CookieService;
        public readonly AppSettings Settings;

        public BaseController(ICookie cookieService, IOptions<AppSettings> options)
        {
            //_httpContextAccessor = httpContextAccessor;
            CookieService = cookieService;
            Settings = options.Value;
        }
    }
}
