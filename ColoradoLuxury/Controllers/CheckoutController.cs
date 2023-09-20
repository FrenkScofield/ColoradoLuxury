using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
