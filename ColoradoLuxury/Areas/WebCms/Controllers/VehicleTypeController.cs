﻿using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;
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
        public async Task<IActionResult> AddVehicleType(string vehicleType, string permile)
        {
            if (ModelState.IsValid)
            {
                if (decimal.TryParse(permile, out decimal result))
                {
                    result = decimal.Parse(permile);
                }
                else
                {
                    ModelState.AddModelError("PerMile", "Value not valid");
                    return View();
                }
                if (vehicleType != null)
                {

                    string perMileTake2digit = Convert.ToDecimal(permile).ToString("F2");
                    VehicleType typName = new VehicleType()
                    {
                        TypeName = vehicleType,
                        PerMile = result,
                        IsActive = false
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

            VehicleTypeDetailsVM model = new VehicleTypeDetailsVM() { Id = vehicleType.Id, TypeName = vehicleType.TypeName, PerMile = vehicleType.PerMile.ToString("F2"), IsActive = vehicleType.IsActive };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditVehicleType(VehicleTypeDetailsVM vehicleType)
        {
            if (vehicleType == null) return NotFound();

            if (decimal.TryParse(vehicleType.PerMile, out decimal permile))
            {
                permile = decimal.Parse(vehicleType.PerMile);
            }
            else
            {
                ModelState.AddModelError("PerMile", "Value not valid");
                return View(vehicleType);
            }

            VehicleType? vehicleT = await _context.VehicleTypes.FindAsync(vehicleType.Id);

            if (vehicleT == null) return NotFound();

            vehicleT.TypeName = vehicleType.TypeName;
            vehicleT.PerMile = permile;
            vehicleT.IsActive = vehicleType.IsActive;

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

            var defaultVehicleType = _context.VehicleTypes.Where(x => x.IsActive == true).FirstOrDefault();

            if (defaultVehicleType != null)
            {
                defaultVehicleType.IsActive = false;
                _context.VehicleTypes.Update(defaultVehicleType);
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
    }
}
