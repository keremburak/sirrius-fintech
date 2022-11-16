using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sirrius.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirrius.WebUI.Views.Shared.Components.NavigationMenu
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NavigationMenuViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IViewComponentResult Invoke()
        {
            //var menus = NavigationModel.Full;
            var menus = NavigationModel.BuildNavigation(_httpContextAccessor);

            return View(menus);
        }
    }
}
