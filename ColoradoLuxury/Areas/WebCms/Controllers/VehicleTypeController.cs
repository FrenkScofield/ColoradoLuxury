using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class VehicleTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddVehicleType()
        {
            return View();
        }

        public IActionResult EditVehicleType()
        {
            return View();
        }
    }
}
