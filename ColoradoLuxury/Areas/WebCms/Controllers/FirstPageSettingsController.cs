using ColoradoLuxury.Attributes;
using ColoradoLuxury.Extensions;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class FirstPageSettingsController : AuthController
    {
        const decimal validationErrorCode = -1;
        private readonly ColoradoContext _context;
        public FirstPageSettingsController(ColoradoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var getDefineMinimumAmountAndDistanceSize = _context.DefineMinimumAmountAndDistanceSizes.FirstOrDefault();

            DefineMinimumAmountsAndDistanceVM model = new DefineMinimumAmountsAndDistanceVM()
            {
                MinimumMile = getDefineMinimumAmountAndDistanceSize?.MinimumMile.ToString(),
                MinimumBookingvalueForDistance = getDefineMinimumAmountAndDistanceSize?.MinimumBookingvalueForDistance.ToString(),
                MinimumDuration = getDefineMinimumAmountAndDistanceSize?.MinimumDuration,
                MinimumBookingvalueForHourly =getDefineMinimumAmountAndDistanceSize?.MinimumBookingvalueForHourly.ToString()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DefineMinimumAmountsAndDistanceVM model)
        {
            byte validationErrorCountCount = 0;
            decimal MinimumMile = model.MinimumMile.TryParseDecimal();
            decimal MinimumBookingvalueForDistance = model.MinimumBookingvalueForDistance.TryParseDecimal();
            decimal MinimumBookingvalueForHourly = model.MinimumBookingvalueForHourly.TryParseDecimal();

            if (model.MinimumDuration < 0)
            {
                ModelState.AddModelError("DurationValue", "Duration value not valid");
                validationErrorCountCount++;
            }

            if (MinimumMile == validationErrorCode)
            {
                ModelState.AddModelError("MinimumMile", "Mile not valid");
                validationErrorCountCount++;
            }

            if (MinimumBookingvalueForDistance == validationErrorCode)
            {
                ModelState.AddModelError("MinimumBookingvalueForDistance", "Mile not valid");
                validationErrorCountCount++;
            }

            if (MinimumBookingvalueForHourly == validationErrorCode)
            {
                ModelState.AddModelError("MinimumBookingvalueForHourly", "Mile not valid");
                validationErrorCountCount++;
            }

            if (validationErrorCountCount > 0)
                return View(model);

            if (!_context.DefineMinimumAmountAndDistanceSizes.Any())
            {
                DefineMinimumAmountAndDistanceSize defineMinimumAmountAndDistanceSize = new DefineMinimumAmountAndDistanceSize();
                defineMinimumAmountAndDistanceSize.MinimumMile = MinimumMile;
                defineMinimumAmountAndDistanceSize.MinimumBookingvalueForDistance = MinimumBookingvalueForDistance;
                defineMinimumAmountAndDistanceSize.MinimumDuration = model.MinimumDuration;
                defineMinimumAmountAndDistanceSize.MinimumBookingvalueForHourly = MinimumBookingvalueForHourly;

                _context.DefineMinimumAmountAndDistanceSizes.Add(defineMinimumAmountAndDistanceSize);
                _context.SaveChanges();

            }
            else
            {
                var getDefineMinimumAmountAndDistanceSize = _context.DefineMinimumAmountAndDistanceSizes.FirstOrDefault();
                getDefineMinimumAmountAndDistanceSize.MinimumMile = MinimumMile;
                getDefineMinimumAmountAndDistanceSize.MinimumBookingvalueForDistance = MinimumBookingvalueForDistance;
                getDefineMinimumAmountAndDistanceSize.MinimumDuration = model.MinimumDuration;
                getDefineMinimumAmountAndDistanceSize.MinimumBookingvalueForHourly = MinimumBookingvalueForHourly;

                _context.DefineMinimumAmountAndDistanceSizes.Update(getDefineMinimumAmountAndDistanceSize);
                _context.SaveChanges();
            }


            return View(model);
        }


    }
}
