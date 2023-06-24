using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class FirstPageSettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddVehicle()
        {
            return View();
        }
    }
}
