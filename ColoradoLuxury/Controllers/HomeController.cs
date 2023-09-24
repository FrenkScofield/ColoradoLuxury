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
            string? distanceAmount = null;
            string? gratuity = null;
            string? totalAmount = null;
            List<VehicleAmounts> vehicleAmountsList = new List<VehicleAmounts>();
            VehicleAmounts? vehicleAmounts = null;
            List<GetVehicleDistanceAmounts> getVehicleDistanceAmounts = new List<GetVehicleDistanceAmounts>();
            List<VehicleType>? vehicleTypes = null;
            VehicleAmounts? getVehiclesIsActive = null;
            List<GetVehicleDistanceAmounts>? getVehiclesPermileValues = null;
            if (hourly && durationValue == 0)
            {
                vehicleTypes = _context.VehicleTypes.Select(x => new VehicleType
                {
                    TypeName = x.TypeName,
                    PerMile = x.PerMile,
                    IsActive = x.IsActive,
                }).ToList();

                if (vehicleTypes.Count == 0)                
                    return Json(new
                    {
                        vehicleTypeNotFound = true

                    });
                

                //get per mile for vehicles
                vehicleTypes.SetPermileValues(_httpContextAccessor.HttpContext);

                var mile = HttpContext?.Session?.GetString("mile");
                var hours = HttpContext?.Session?.GetInt32("hours");
                var minutes = HttpContext?.Session?.GetInt32("minutes");

                if (mile != null)
                {
                    getVehiclesPermileValues = vehicleTypes.GetPermileValues(_httpContextAccessor.HttpContext);


                    foreach (var getVehiclesPermileValue in getVehiclesPermileValues)
                    {
                        distanceAmount = (Convert.ToDecimal(mile) * decimal.Parse(getVehiclesPermileValue.DistanceAmount)).ToString("F2");
                        if (Convert.ToDecimal(distanceAmount) <= 80)
                            distanceAmount = "80";

                        gratuity = (Convert.ToDecimal(distanceAmount) * 0.15m).ToString("F2");
                        totalAmount = (Convert.ToDecimal(distanceAmount) + Convert.ToDecimal(gratuity)).ToString("F2");

                        getVehicleDistanceAmounts.Add(new GetVehicleDistanceAmounts() { Key = getVehiclesPermileValue.Key, DistanceAmount = distanceAmount, IsActive = getVehiclesPermileValue.IsActive });

                        vehicleAmounts = new VehicleAmounts
                        {
                            DistanceAmount = distanceAmount,
                            Graduity = gratuity,
                            TotalAmount = totalAmount,
                            IsActive= getVehiclesPermileValue.IsActive
                        };
                        vehicleAmountsList?.Add(vehicleAmounts);
                        HttpContext?.Session?.SetObjectsession($"{getVehiclesPermileValue.Key}-result", vehicleAmounts);
                    }

                    getVehiclesIsActive = vehicleAmountsList.Where(x => x.IsActive == true).FirstOrDefault();

                    if (vehicleTypes != null && vehicleTypes.Any(x => x.IsActive == true))
                        HttpContext?.Session?.SetObjectsession("activeVehicleAmountSession", getVehiclesIsActive);
                }
            }
            else
            {
                if (durationValue <= 4)
                    durationValue = 4;
                distanceAmount = (durationValue * 50).ToString("F2");


                gratuity = (Convert.ToDecimal(distanceAmount) * 0.15m).ToString("F2");
                totalAmount = (Convert.ToDecimal(distanceAmount) + Convert.ToDecimal(gratuity)).ToString("F2");

                HttpContext?.Session?.SetString("distanceAmount", distanceAmount);
                HttpContext?.Session?.SetString("gratuity", gratuity);
                HttpContext?.Session?.SetString("totalAmount", totalAmount);
            }

            return Json(new
            {
                getVehicleDistanceAmounts,
                getVehiclesIsActive,
                //distanceAmount = distanceAmount,
                //gratuity = gratuity,
                //totalAmount = totalAmount

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

            if (model.Hours <= 2)
                model.Hours = 2;

            string mile = model.Mile.ToString();
            HttpContext.Session.SetString("mile", mile);
            HttpContext.Session.SetInt32("hours", model.Hours);
            HttpContext.Session.SetInt32("minutes", model.Minutes);


            return Json(new { });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}