﻿using ColoradoLuxury.Enums;
using ColoradoLuxury.Extensions;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Org.BouncyCastle.Asn1.X509;

namespace ColoradoLuxury.Controllers
{
    public class RideDetailsController : Controller
    {
        private readonly ColoradoContext _context;
        private readonly IMemoryCache _cache;

        public RideDetailsController(ColoradoContext context, IMemoryCache cache)
        {
            _context = context ?? throw new ArgumentNullException("Context did not find");
            _cache = cache;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDetails([FromBody] RideDetailsVM model)
        {
            DateTime endDate = DateTime.MinValue;
            if (model.DurationInHours != null)
            {
                 endDate = TimeRangeGenerator.GetEndDateTime(model.PickupTime, model.DurationInHours);
                 model.EndPickupTime = endDate.ToString("HH:mm");
            }

            HttpContext.SetObjectsession("firstridedetails", model);

            return Json(new { status = Ok() });

        }

        [HttpPost]
        public IActionResult AddVehiclesInfo([FromBody] ChooseVehicleVM model)
        {
            HttpContext.SetObjectsession("vehicleDetails", model);
            var keys = HttpContext.Session.Keys;
            var rideDetails = HttpContext.GetObjectsession<RideDetailsVM>("firstridedetails");
            //var rideDetails = _cache.Get<RideDetailsVM>("firstridedetailscache");
            string text = "Denver International Airport (DEN), Peña Boulevard, Denver, CO, USA";
            bool airlineAutoCheck = false;
            if (rideDetails != null)
            {
                if (rideDetails.DropOffLocation == text || rideDetails.PickupLocation == text)
                    airlineAutoCheck = true;
            }

            return Json(new { airlineAutoCheck, status = Ok() });

        }

        [HttpPost]
        public IActionResult AddContactDetailsInfo([FromBody] ContactDetailsVM model)
        {
            if (model.AirlineAutoCheck && !model.AirLineStatus)
            {
                return Json(new
                {
                    wrongStatus = true
                });
            }

            string? wayType = null;
            HttpContext.SetObjectsession("contactDetails", model);

            //Get All Informations
            var rideDetails = HttpContext.GetObjectsession<RideDetailsVM>("firstridedetails");
            //var rideDetails = _cache.Get<RideDetailsVM>("firstridedetailscache");
            var vehicleDetails = HttpContext.GetObjectsession<ChooseVehicleVM>("vehicleDetails");
            var contactDetails = HttpContext.GetObjectsession<ContactDetailsVM>("contactDetails");

            if (rideDetails.WayType)
                wayType = WayTypeEnum.Distance.ToString();
            else
                wayType = WayTypeEnum.Hourly.ToString();
            GeneralInformationAboutRide getTextForIdVM = null;
            try
            {
                getTextForIdVM = new GeneralInformationAboutRide()
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
            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(new
            {
                rideDetails,
                vehicleDetails,
                contactDetails,
                getTextForIdVM,
                status = Ok() 
            });

        }

        [HttpPost]
        public async Task<IActionResult> GetAmountByVehicle(string sessionName)
        {
            VehicleAmounts? amount = HttpContext.GetObjectsession<VehicleAmounts>($"{sessionName}-result");

            var getAllVehicleTypes = _context.VehicleTypes.ToList();
            foreach (var getAllVehicleType in getAllVehicleTypes)
            {
                if (HttpContext.GetObjectsession<VehicleAmounts>($"{getAllVehicleType.TypeName.Replace(" ", "").ToLower()}-result").IsActive == true)
                {

                    var previusAmountResultSession = HttpContext.GetObjectsession<VehicleAmounts>($"{getAllVehicleType.TypeName.Replace(" ", "").ToLower()}-result");
                    HttpContext.Session.Remove($"{getAllVehicleType.TypeName.Replace(" ", "").ToLower()}-result");

                    var VehicleAmountsForUpdatePreviusSession = new VehicleAmounts()
                    {
                        DistanceAmount = previusAmountResultSession.DistanceAmount,
                        Graduity = previusAmountResultSession.Graduity,
                        TotalAmount = previusAmountResultSession.TotalAmount,
                        VehicleTypeId = previusAmountResultSession.VehicleTypeId,
                        IsActive = false
                    };

                    HttpContext.SetObjectsession($"{getAllVehicleType.TypeName.Replace(" ", "").ToLower()}-result", VehicleAmountsForUpdatePreviusSession);

                }

                getAllVehicleType.IsActive = false;

                _context.VehicleTypes.Update(getAllVehicleType);
                _context.SaveChanges();
            }

            var getVehicleType = _context.VehicleTypes.Where(x => x.Id == amount.VehicleTypeId).FirstOrDefault();

            getVehicleType.IsActive = true;
            amount.IsActive = getVehicleType.IsActive;
            var currentAmountResultSession = HttpContext.GetObjectsession<VehicleAmounts>($"{getVehicleType.TypeName.Replace(" ", "").ToLower()}-result");
            HttpContext.Session.Remove($"{getVehicleType.TypeName.Replace(" ", "").ToLower()}-result");
            var VehicleAmountsForUpdateCurrentSession = new VehicleAmounts()
            {
                DistanceAmount = currentAmountResultSession.DistanceAmount,
                Graduity = currentAmountResultSession.Graduity,
                TotalAmount = currentAmountResultSession.TotalAmount,
                VehicleTypeId = currentAmountResultSession.VehicleTypeId,
                IsActive = getVehicleType.IsActive
            };
            HttpContext.SetObjectsession($"{getVehicleType.TypeName.Replace(" ", "").ToLower()}-result", VehicleAmountsForUpdateCurrentSession);
            _context.VehicleTypes.Update(getVehicleType);
            _context.SaveChanges();

            //HttpContext.Session.Remove($"{getAllVehicleType.TypeName.Replace(" ", "").ToLower()}-result");

            //VehicleAmounts? amount = new VehicleAmounts { IsActive = false, DistanceAmount = getAllVehicleType };
            //HttpContext.SetObjectsession($"{getAllVehicleType.TypeName.Replace(" ", "").ToLower()}-result", amount);

            if (HttpContext.GetObjectsession<VehicleAmounts>("activeVehicleAmountSession") != null)
            {
                HttpContext.Session.Remove("activeVehicleAmountSession");
            }
            HttpContext.SetObjectsession("activeVehicleAmountSession", amount);

            return Json(new { amount });

        }
    }
}
