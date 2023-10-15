using ColoradoLuxury.Attributes;
using ColoradoLuxury.Models.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class BookingSystemController : AuthController
    {
        private readonly ColoradoContext _context;
        public BookingSystemController(ColoradoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeStatusForHourlyButton(bool status)
        {
            return View();
        }
    }
}
