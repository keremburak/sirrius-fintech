using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirrius.WebUI.Models
{
    public class AjaxResultModel
    {
        public bool done { get; set; } = false;
        public object data { get; set; } = null;
        public string message { get; set; } = "";
        public int httpStatusCode { get; set; }
        public bool isRedirectRequired { get; set; }
        public string redirectUrl { get; set; } = null;
    }
}
