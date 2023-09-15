using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class VehicleTypeController : Controller
    {
        private readonly ColoradoContext _context;
        public VehicleTypeController(ColoradoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleTypes.ToListAsync());
        }

        public IActionResult AddVehicleType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleType(string vehicleType)
        {
            if (ModelState.IsValid)
            {
                if (vehicleType != null)
                {
                    VehicleType typName = new VehicleType()
                    {
                        TypeName = vehicleType,
                    };

                  _context.VehicleTypes.Add(typName);
                }
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditVehicleType()
        {
            return View();
        }
    }
}
