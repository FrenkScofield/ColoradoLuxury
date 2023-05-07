using Microsoft.AspNetCore.Mvc;


namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class UsersRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
