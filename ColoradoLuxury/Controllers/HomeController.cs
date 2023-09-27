using ColoradoLuxury.Extensions;
using ColoradoLuxury.FluentValidation;
using ColoradoLuxury.Models;
using ColoradoLuxury.Models.BLL;
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
        protected internal IHttpContextAccessor _httpContextAccessor;


        public HomeController(ILogger<HomeController> logger, ColoradoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            HomeInfoDetailsVM viewModel = new HomeInfoDetailsVM()
            {
                VehicleTypes = _context.VehicleTypes.ToList(),
                TransferTypes = _context.TransferTypes.ToList(),
                Countries = _context.Countries.ToList(),
                AirLines = _context.AirLines.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult CalculatedAmount(bool hourly, short durationValue)
        {
            GetVehiclesAmountDetailsVM? getVehiclesAmountDetails = null;
            List<VehicleType>? vehicleTypes = _context.VehicleTypes.Select(x => new VehicleType
            {
                TypeName = x.TypeName,
                PerMile = x.PerMile,
                Hourly = x.Hourly,
                IsActive = x.IsActive,
            }).ToList();

            if (hourly && durationValue == 0)
            {
                if (vehicleTypes.Count == 0)
                    return Json(new { vehicleTypeNotFound = true });

                var mile = SessionExtension.GetSessionString(_httpContextAccessor.HttpContext, "mile");

                var hours = SessionExtension.GetSessionInt32(_httpContextAccessor.HttpContext, "hours");

                var minutes = SessionExtension.GetSessionInt32(_httpContextAccessor.HttpContext, "minutes");

                if (mile != null)
                    getVehiclesAmountDetails = CalculateForDistanceOrHourly(hourly, mile, 80, vehicleTypes, 30.5);
                else
                    return Json(new { NotFoundMileValue = true });
            }
            else
                getVehiclesAmountDetails = CalculateForDistanceOrHourly(hourly, durationValue, 100, vehicleTypes, 4);


            return Json(new
            {
                getVehiclesAmountDetails.GetVehicleDistanceAmounts,
                getVehiclesAmountDetails.GetVehiclesIsActive,
            });
        }

        public GetVehiclesAmountDetailsVM CalculateForDistanceOrHourly(bool distanceType, dynamic minimumTravelValueType, dynamic distanceTypeValue, List<VehicleType>? vehicleTypes, object checkingMinimumTravelValueTypeFromDbValue)
        {
            List<GetVehicleDistanceAmounts> getVehicleDistanceAmounts = new List<GetVehicleDistanceAmounts>();
            List<VehicleAmounts> vehicleAmountsList = new List<VehicleAmounts>();
            List<GetVehicleDistanceAmounts>? getVehiclesPermileOrHourValues = null;

            if (distanceType)
            {
                distanceTypeValue = Convert.ToDecimal(distanceTypeValue);
                if (Convert.ToDecimal(minimumTravelValueType) < Convert.ToDecimal(checkingMinimumTravelValueTypeFromDbValue))
                {
                    minimumTravelValueType = Convert.ToDecimal(checkingMinimumTravelValueTypeFromDbValue);
                }

                vehicleTypes?.SetPermileValues(_httpContextAccessor.HttpContext);
                getVehiclesPermileOrHourValues = vehicleTypes.GetPermileOrValues(_httpContextAccessor.HttpContext);

            }
            else
            {
                distanceTypeValue = (short)distanceTypeValue;

                if ((Int32)minimumTravelValueType  < (Int32)checkingMinimumTravelValueTypeFromDbValue)
                {
                    minimumTravelValueType = (Int32)checkingMinimumTravelValueTypeFromDbValue;
                }

                //set hourly for vehicles
                vehicleTypes?.SetByHourValues(_httpContextAccessor.HttpContext);
                getVehiclesPermileOrHourValues = vehicleTypes.GetPermileOrValues(_httpContextAccessor.HttpContext);
            }

            foreach (var getVehiclesPermileValue in getVehiclesPermileOrHourValues)
            {
                string? distanceAmount = (minimumTravelValueType * decimal.Parse(getVehiclesPermileValue.DistanceAmount)).ToString("F2");
                if (Convert.ToDecimal(distanceAmount) <= distanceTypeValue)
                    distanceAmount = GeneralExtension<decimal>.ToString(distanceTypeValue);

                string? gratuity = (Convert.ToDecimal(distanceAmount) * 0.15m).ToString("F2");
                string? totalAmount = (Convert.ToDecimal(distanceAmount) + Convert.ToDecimal(gratuity)).ToString("F2");

                getVehicleDistanceAmounts.Add(new GetVehicleDistanceAmounts() { Key = getVehiclesPermileValue.Key, DistanceAmount = distanceAmount, IsActive = getVehiclesPermileValue.IsActive });

                VehicleAmounts? vehicleAmounts = new VehicleAmounts
                {
                    DistanceAmount = distanceAmount,
                    Graduity = gratuity,
                    TotalAmount = totalAmount,
                    IsActive = getVehiclesPermileValue.IsActive
                };
                vehicleAmountsList?.Add(vehicleAmounts);
                HttpContext?.Session?.SetObjectsession($"{getVehiclesPermileValue.Key}-result", vehicleAmounts);
            }

            VehicleAmounts? getVehiclesIsActive = vehicleAmountsList.Where(x => x.IsActive == true).FirstOrDefault();

            if (vehicleTypes != null && vehicleTypes.Any(x => x.IsActive == true))
                HttpContext?.Session?.SetObjectsession("activeVehicleAmountSession", getVehiclesIsActive);


            GetVehiclesAmountDetailsVM model = new GetVehiclesAmountDetailsVM()
            {
                GetVehicleDistanceAmounts = getVehicleDistanceAmounts,
                GetVehiclesIsActive = getVehiclesIsActive
            };

            return model;
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

            if (model.Hours <= 2)
                model.Hours = 2;

            string mile = model.Mile.ToString();

            SessionExtension.SetSessionString(_httpContextAccessor.HttpContext, "mile", mile);

            SessionExtension.SetSessionInt32(_httpContextAccessor.HttpContext, "hours", model.Hours);

            SessionExtension.SetSessionInt32(_httpContextAccessor.HttpContext, "minutes", model.Minutes);


            return Json(new { });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}