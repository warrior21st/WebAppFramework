using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Manage.Attributes;

namespace WebApp.Areas.Manage.Controllers
{
    public class HomeController:ManageBaseController
    {
        public HomeController(ILogger<HomeController> logger,IServiceProvider serviceProvider):base(logger,serviceProvider)
        {

        }

        [Area("Manage")]
        [ManageAuthorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
