using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class CalculationMethotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
