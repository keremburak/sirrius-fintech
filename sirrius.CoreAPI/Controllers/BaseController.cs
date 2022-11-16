using Microsoft.AspNetCore.Mvc;
using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirrius.CoreAPI.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public User MyUser => (User)HttpContext.Items["User"];
    }
}
