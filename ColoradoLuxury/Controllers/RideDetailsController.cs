using ColoradoLuxury.Enums;
using ColoradoLuxury.Extensions;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Controllers
{
    public class RideDetailsController : Controller
    {
        private readonly ColoradoContext _context;

        public RideDetailsController(ColoradoContext context)
        {
            _context = context ?? throw new ArgumentNullException("Context did not find");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDetails([FromBody] RideDetailsVM model)
        {
            HttpContext.Session.SetObjectsession("rideDetails", model);

            return Json(new { });

        }

        [HttpPost]
        public IActionResult AddVehiclesInfo([FromBody] ChooseVehicleVM model)
        {
            HttpContext.Session.SetObjectsession("vehicleDetails", model);

            return Json(new { });

        }

        [HttpPost]
        public IActionResult AddContactDetailsInfo([FromBody] ContactDetailsVM model)
        {
            string? wayType = null;
            HttpContext.Session.SetObjectsession("contactDetails", model);

            //Get All Informations
            var rideDetails = HttpContext.Session.GetObjectsession<RideDetailsVM>("rideDetails");
            var vehicleDetails = HttpContext.Session.GetObjectsession<ChooseVehicleVM>("vehicleDetails");
            var contactDetails = HttpContext.Session.GetObjectsession<ContactDetailsVM>("contactDetails");

            if (rideDetails.WayType)
                wayType = WayTypeEnum.Distance.ToString();
            else
                wayType = WayTypeEnum.Hourly.ToString();

            GeneralInformationAboutRide getTextForIdVM = new GeneralInformationAboutRide()
            {
                TransferType = _context.TransferTypes.Where(tp => tp.Id == rideDetails.TransferTypeId).FirstOrDefault()?.Name,
                VehicleType = _context.VehicleTypes.Where(tp => tp.Id == vehicleDetails.AllcarTpes).FirstOrDefault()?.TypeName,
                Country = _context.Countries.Where(tp => tp.Id == contactDetails.CountryId).FirstOrDefault()?.Name,
                AirLine = _context.AirLines.Where(tp => tp.Id == contactDetails.AirlineId).FirstOrDefault()?.Name,
                WayType = wayType,
                PickupDateAndTime = rideDetails.GetPickupDateAndTime(),
                TotalDistance = $"{Convert.ToDecimal(HttpContext.Session.GetString("mile"))} mi",
                DistanceTime = $"{HttpContext.Session.GetInt32("hours")}h {HttpContext.Session.GetInt32("minutes")}m" 
            };
            return Json(new
            {
                rideDetails,
                vehicleDetails,
                contactDetails,
                getTextForIdVM
            });

        }
    }
}
