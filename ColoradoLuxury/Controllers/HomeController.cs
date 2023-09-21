﻿using ColoradoLuxury.FluentValidation;
using ColoradoLuxury.Models;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ColoradoLuxury.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ColoradoContext _context;

        public HomeController(ILogger<HomeController> logger, ColoradoContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            HomeInfoDetailsVM viewModel = new HomeInfoDetailsVM() {
                VehicleTypes = _context.VehicleTypes.ToList(),
                TransferTypes= _context.TransferTypes.ToList(),
                Countries= _context.Countries.ToList(),
                AirLines = _context.AirLines.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult CalculatedAmount(bool hourly, short durationValue)
        {
            string? distanceAmount = null;
            string? gratuity = null;
            string? totalAmount = null;
            if (hourly && durationValue == 0)
            {
                var mile = HttpContext?.Session?.GetString("mile");
                var hours = HttpContext?.Session?.GetInt32("hours");
                var minutes = HttpContext?.Session?.GetInt32("minutes");

                if (mile != null)
                {
                    distanceAmount = (Convert.ToDecimal(mile) * 3.50m).ToString("F2");
                    if (Convert.ToDecimal(distanceAmount) <= 10)
                        distanceAmount= "10";

                    gratuity = (Convert.ToDecimal(distanceAmount) * 0.15m).ToString("F2");
                    totalAmount = (Convert.ToDecimal(distanceAmount) + Convert.ToDecimal(gratuity)).ToString("F2");

                    HttpContext?.Session?.SetString("distanceAmount", distanceAmount);
                    HttpContext?.Session?.SetString("gratuity", gratuity);
                    HttpContext?.Session?.SetString("totalAmount", totalAmount);
                }
            }
            else
            {
                distanceAmount = (durationValue * 50).ToString("F2");
                if (Convert.ToDecimal(distanceAmount) <= 10)
                    distanceAmount = "10";

                gratuity = (Convert.ToDecimal(distanceAmount) * 0.15m).ToString("F2");
                totalAmount = (Convert.ToDecimal(distanceAmount) + Convert.ToDecimal(gratuity)).ToString("F2");

                HttpContext?.Session?.SetString("distanceAmount", distanceAmount);
                HttpContext?.Session?.SetString("gratuity", gratuity);
                HttpContext?.Session?.SetString("totalAmount", totalAmount);
            }

            return Json(new
            {
                distanceAmount = distanceAmount,
                gratuity = gratuity,
                totalAmount = totalAmount

            });
        }

        [HttpPost]
        public IActionResult GetDistanceAndTime([FromBody] GetMileAndTime model)
        {
            if (!ModelState.IsValid)
            {
                var modelstate = ModelState.ToList();
                string? errormMessage = null;
                foreach (var error in modelstate)
                {
                    errormMessage = error.Value.Errors[0].ErrorMessage;
                }
                return Json(new
                {

                    wrongSomething = errormMessage
                }); ;

            }

            if (model.Mile <= 10)
                model.Mile = 10;

            if(model.Hours <= 2)
                model.Hours = 2;

            string mile = model.Mile.ToString();
            HttpContext.Session.SetString("mile", mile);
            HttpContext.Session.SetInt32("hours", model.Hours);
            HttpContext.Session.SetInt32("minutes", model.Minutes);


            return Json(new{});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}