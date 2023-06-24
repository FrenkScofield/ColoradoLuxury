using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class RandomCoupon : Controller
    {
        public IActionResult Coupon()
        {
            return View();
        }
    }
}
