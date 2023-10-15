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
    public class ChooseVehiclePageController : AuthController
    {
        private readonly ColoradoContext _context;
        public ChooseVehiclePageController(ColoradoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Vehicles.Include(x => x.VehicleType).OrderByDescending(x => x.Id).ToList());
        }

        public IActionResult AddVehicle()
        {
            VehicleBrandVM model = new VehicleBrandVM()
            {
                VehicleTypes = _context.VehicleTypes.ToList(),
                Vehicle = new Models.BLL.Vehicle()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddVehicle(VehicleBrandVM model)
        {

            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState.ToList())
                {
                    foreach (var error in item.Value.Errors)
                    {
                        ModelState.AddModelError(item.Key, error.ErrorMessage);
                    }
                }
                return View(model);
            }

            Vehicle vehicle = new Vehicle()
            {
                Name = model.Vehicle.Name,
                Model = model.Vehicle.Model,
                Engine = model.Vehicle.Engine,
                Fuel = model.Vehicle.Fuel,
                Color = model.Vehicle.Color,
                VehicleTypeId = model.Vehicle.VehicleTypeId
            };

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditVehicle(int id)
        {
            if (id == 0)
                return NotFound();


            VehicleBrandVM model = new VehicleBrandVM()
            {
                VehicleTypes = _context.VehicleTypes.ToList(),
                Vehicle = _context.Vehicles.Include(x => x.VehicleType).Where(y => y.Id == id).FirstOrDefault()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditVehicle(int id, VehicleBrandVM model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState.ToList())
                {
                    foreach (var error in item.Value.Errors.ToList())
                    {
                        ModelState.AddModelError(item.Key, error.ErrorMessage);
                    }
                }
                model.VehicleTypes = _context.VehicleTypes.ToList();
                return View(model);
            }

            var previusData = _context.Vehicles.Where(x => x.Id == id).FirstOrDefault();

            previusData.Name = model.Vehicle.Name;
            previusData.Model = model.Vehicle.Model;
            previusData.Engine = model.Vehicle.Engine;
            previusData.Fuel = model.Vehicle.Fuel;
            previusData.Color = model.Vehicle.Color;
            previusData.VehicleTypeId = model.Vehicle.VehicleTypeId;

            _context.Vehicles.Update(previusData);
            _context.SaveChanges();

            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (id == 0) return NotFound();

            Vehicle vehicleType = await _context.Vehicles.FindAsync(id);

            if (vehicleType == null) return NotFound();

            _context.Vehicles.Remove(vehicleType);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
