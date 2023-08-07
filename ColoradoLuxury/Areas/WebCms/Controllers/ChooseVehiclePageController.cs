using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class ChooseVehiclePageController : Controller
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
