using ColoradoLuxury.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/")]
    public class HomeController : AuthController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
