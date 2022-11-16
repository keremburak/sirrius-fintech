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
    public class ErrorController : BaseController
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger, ICookie cookieService, IOptions<AppSettings> options)
            : base(cookieService, options)
        {
            _logger = logger;
        }

        public IActionResult Unauthorized401() => View("Unauthorized");
    }
}
