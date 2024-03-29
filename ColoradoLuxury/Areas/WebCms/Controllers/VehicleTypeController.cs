using ColoradoLuxury.Attributes;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class VehicleTypeController : AuthController
    {
        private readonly ColoradoContext _context;
        public VehicleTypeController(ColoradoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vehicleType = await _context.VehicleTypes.ToListAsync();
            if (vehicleType.Count == 1 && !vehicleType[0].IsActive)
            {
                var defaultVehicleType = await _context.VehicleTypes.ToListAsync();

                if (defaultVehicleType != null)
                {
                    foreach (var defVehicleType in defaultVehicleType)
                    {
                        defVehicleType.IsActive = false;
                        _context.VehicleTypes.Update(defVehicleType);
                        await _context.SaveChangesAsync();
                    }

                }

                vehicleType[0].IsActive = true;
                _context.VehicleTypes.Update(vehicleType[0]);
                _context.SaveChanges();
            }

            return View(vehicleType);
        }

        
        public async Task<IActionResult> ShowOrNotShowStatus(int id)
        {
            var vehicleStatus = await _context.VehicleTypes.FindAsync(id);
 
                if (vehicleStatus.Status == true)
                {
                    vehicleStatus.Status = false;
                }
                else
                {
                    vehicleStatus.Status = true;
                }
            _context.VehicleTypes.Update(vehicleStatus);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult AddVehicleType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleType(string TypeName, string PerMile, string Hourly)
        {
            if (ModelState.IsValid)
            {
                if (decimal.TryParse(PerMile, out decimal PerMileResult) && decimal.TryParse(Hourly, out decimal HourlyResult))
                {
                    PerMileResult = decimal.Parse(PerMile);
                    HourlyResult = decimal.Parse(Hourly);

                }
                else
                {
                    ModelState.AddModelError("PerMile", "Value not valid");
                    return View();
                }
                if (TypeName != null)
                {

                    string perMileTake2digit = Convert.ToDecimal(PerMile).ToString("F2");
                    VehicleType typName = new VehicleType()
                    {
                        TypeName = TypeName,
                        PerMile = PerMileResult,
                        Hourly = HourlyResult,
                        Status = true
                    };

                    if (_context.VehicleTypes.Count() == 0)
                        typName.IsActive = true;


                    _context.VehicleTypes.Add(typName);
                }
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> EditVehicleType(int Id)
        {
            if (Id == 0) return NotFound();

            VehicleType? vehicleType = await _context.VehicleTypes.FindAsync(Id);

            if (vehicleType == null) return NotFound();



            VehicleTypeDetailsVM model = new VehicleTypeDetailsVM() { Id = vehicleType.Id, Hourly = vehicleType.Hourly.ToString("F3"), TypeName = vehicleType.TypeName, PerMile = vehicleType.PerMile.ToString("F2"), IsActive = vehicleType.IsActive };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditVehicleType(VehicleTypeDetailsVM vehicleType)
        {
            if (vehicleType == null) return NotFound();

            if (decimal.TryParse(vehicleType.PerMile, out decimal permile) && decimal.TryParse(vehicleType.Hourly, out decimal hourly))
            {
                permile = decimal.Parse(vehicleType.PerMile);
                hourly = decimal.Parse(vehicleType.Hourly);
            }
            else
            {
                ModelState.AddModelError("PerMile or Hourly", "Value not valid");
                return View(vehicleType);
            }

            VehicleType? vehicleT = await _context.VehicleTypes.FindAsync(vehicleType.Id);

            if (vehicleT == null) return NotFound();

            vehicleT.TypeName = vehicleType.TypeName;
            vehicleT.PerMile = permile;
            vehicleT.IsActive = vehicleType.IsActive;
            vehicleT.Hourly = hourly;



            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public IActionResult SetAsDefaultVehicleType(int id)
        {
            if (id <= 0)
                return Json(new
                {
                    BadRequest = true
                });

            var vehicleType = _context.VehicleTypes.Where(x => x.Id == id).FirstOrDefault();

            if (vehicleType == null) return NotFound();

            var defaultVehicleType = _context.VehicleTypes.Where(x => x.IsActive == true).ToList();

            if (defaultVehicleType != null)
            {
                foreach (var defVehicleType in defaultVehicleType)
                {
                    defVehicleType.IsActive = false;
                    _context.VehicleTypes.Update(defVehicleType);
                    _context.SaveChanges();
                }

            }



            vehicleType.IsActive = true;

            _context.VehicleTypes.Update(vehicleType);
            _context.SaveChanges();

            return Json(new
            {
                vehicleType,
                success = true
            });


        }
        [HttpGet]
        public async Task<IActionResult> DeactiveVehicleType(int Id)
        {
            if (Id == 0) return NotFound();

            VehicleType vehicleType = await _context.VehicleTypes.FindAsync(Id);

            if (vehicleType == null) return NotFound();

            vehicleType.Status = false;
            vehicleType.IsActive = false;

            _context.VehicleTypes.Update(vehicleType);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> ActiveVehicleType(int Id)
        {
            if (Id == 0) return NotFound();

            VehicleType vehicleType = await _context.VehicleTypes.FindAsync(Id);

            if (vehicleType == null) return NotFound();

            vehicleType.Status = true;
            vehicleType.IsActive = false;

            _context.VehicleTypes.Update(vehicleType);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

    }
}