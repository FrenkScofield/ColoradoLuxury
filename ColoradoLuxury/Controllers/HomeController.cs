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
            List<string> choosenTimeRange = new List<string>();

            var rideDetails = _context.RideDetails.Select(x => new RideDetail
            {
                PickupTime = x.PickupTime,
                EndPickupTime = x.EndPickupTime,
                DurationId = x.DurationId,
                Duration = x.Duration,
                PickupDate = x.PickupDate

            }).ToList();



            if (rideDetails != null)

            {
                foreach (var rideDetail in rideDetails)
                {
                    if (rideDetail.DurationId != null)
                    {
                        var getTimeRange = TimeRangeGenerator.GenerateTimeRange(rideDetail.PickupDate, rideDetail.PickupTime, rideDetail.EndPickupTime);
                        if (getTimeRange != null)
                        {
                            if (DateTime.Now.Date == getTimeRange[getTimeRange.Count - 1].Date)
                            {
                                foreach (var timeRange in getTimeRange)
                                {
                                    choosenTimeRange.Add(timeRange.TimeOfDay.ToString());
                                }
                            }
                        }
                    }

                }
            }


            HomeInfoDetailsVM viewModel = new HomeInfoDetailsVM()
            {
                VehicleTypes = _context.VehicleTypes.Where(x => x.Status).ToList(),
                TransferTypes = _context.TransferTypes.ToList(),
                Countries = _context.Countries.ToList(),
                AirLines = _context.AirLines.ToList(),
                TimesRange = choosenTimeRange
            };





            return View(viewModel);
        }


        [HttpPost]
        public JsonResult CalculatedAmount(bool hourly, short durationValue)
        {
            GetVehiclesAmountDetailsVM? getVehiclesAmountDetails = null;
            List<VehicleType>? vehicleTypes = _context.VehicleTypes.Where(x => x.Status).Select(x => new VehicleType
            {
                TypeName = x.TypeName,
                PerMile = x.PerMile,
                Hourly = x.Hourly,
                IsActive = x.IsActive,
                Id = x.Id
            }).ToList();

            var gradiuty = _context.ValueOfTipButtons.Select(x => new ValueOfTipButton
            {
                lowInterest = x.lowInterest,
                MiddleInterest = x.MiddleInterest,
                HighInterest = x.HighInterest
            }).FirstOrDefault();

            var bookingSystem = _context.DefineMinimumAmountAndDistanceSizes.Select(x => new DefineMinimumAmountAndDistanceSize
            {
                MinimumMile = x.MinimumMile,
                MinimumDuration = x.MinimumDuration,
                MinimumBookingvalueForDistance = x.MinimumBookingvalueForDistance,
                MinimumBookingvalueForHourly = x.MinimumBookingvalueForHourly
            }).FirstOrDefault();

            if (hourly && durationValue == 0)
            {
                if (vehicleTypes.Count == 0)
                    return Json(new { vehicleTypeNotFound = true });

                var mile = SessionExtension.GetSessionString(_httpContextAccessor.HttpContext, "mile");

                var hours = SessionExtension.GetSessionInt32(_httpContextAccessor.HttpContext, "hours");

                var minutes = SessionExtension.GetSessionInt32(_httpContextAccessor.HttpContext, "minutes");

                if (mile != null)
                    getVehiclesAmountDetails = CalculateForDistanceOrHourly((decimal)gradiuty.lowInterest / 100, hourly, mile, bookingSystem.MinimumBookingvalueForDistance, vehicleTypes, bookingSystem.MinimumMile);
                else
                    return Json(new { NotFoundMileValue = true });
            }
            else
            {
                if (bookingSystem.MinimumBookingvalueForHourly == 0 && bookingSystem.MinimumDuration == null)
                {
                    return Json(new { NotFoundDurationValue = true });
                }
                getVehiclesAmountDetails = CalculateForDistanceOrHourly((decimal)gradiuty.lowInterest / 100, hourly, durationValue, bookingSystem.MinimumBookingvalueForHourly, vehicleTypes, bookingSystem.MinimumDuration);
            }


            return Json(new
            {
                getVehiclesAmountDetails.GetVehicleDistanceAmounts,
                getVehiclesAmountDetails.GetVehiclesIsActive,
            });
        }

        public GetVehiclesAmountDetailsVM CalculateForDistanceOrHourly(decimal gradiuty, bool distanceType, dynamic minimumTravelValueType, dynamic distanceTypeValue, List<VehicleType>? vehicleTypes, object checkingMinimumTravelValueTypeFromDbValue)
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

                if ((Int32)minimumTravelValueType < (Int32)checkingMinimumTravelValueTypeFromDbValue)
                {
                    minimumTravelValueType = (Int32)checkingMinimumTravelValueTypeFromDbValue;
                }

                //set hourly for vehicles
                vehicleTypes?.SetByHourValues(_httpContextAccessor.HttpContext);
                getVehiclesPermileOrHourValues = vehicleTypes.GetPermileOrValues(_httpContextAccessor.HttpContext);
            }

            foreach (var getVehiclesPermileValue in getVehiclesPermileOrHourValues)
            {
                string? distanceAmount = (decimal.Parse(minimumTravelValueType) * decimal.Parse(getVehiclesPermileValue.DistanceAmount)).ToString("F2");
                if (Convert.ToDecimal(distanceAmount) <= distanceTypeValue)
                    distanceAmount = GeneralExtension<decimal>.ToString(distanceTypeValue);

                string? gratuity = (Convert.ToDecimal(distanceAmount) * gradiuty).ToString("F2");
                string? totalAmount = (Convert.ToDecimal(distanceAmount) + Convert.ToDecimal(gratuity)).ToString("F2");

                getVehicleDistanceAmounts.Add(new GetVehicleDistanceAmounts() { Key = getVehiclesPermileValue.Key, DistanceAmount = distanceAmount, IsActive = getVehiclesPermileValue.IsActive, VehicleTypeId = getVehiclesPermileValue.VehicleTypeId });

                VehicleAmounts? vehicleAmounts = new VehicleAmounts
                {
                    DistanceAmount = distanceAmount,
                    Graduity = gratuity,
                    TotalAmount = totalAmount,
                    IsActive = getVehiclesPermileValue.IsActive,
                    VehicleTypeId = getVehiclesPermileValue.VehicleTypeId
                };
                vehicleAmountsList?.Add(vehicleAmounts);
                HttpContext?.SetObjectsession($"{getVehiclesPermileValue.Key}-result", vehicleAmounts);
            }

            VehicleAmounts? getVehiclesIsActive = vehicleAmountsList.Where(x => x.IsActive == true).FirstOrDefault();

            if (vehicleTypes != null && vehicleTypes.Any(x => x.IsActive == true))
                HttpContext?.SetObjectsession("activeVehicleAmountSession", getVehiclesIsActive);


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

            string mile = model.Mile.ToString();

            SessionExtension.SetSessionString(_httpContextAccessor.HttpContext, "mile", mile);

            SessionExtension.SetSessionInt32(_httpContextAccessor.HttpContext, "hours", model.Hours);

            SessionExtension.SetSessionInt32(_httpContextAccessor.HttpContext, "minutes", model.Minutes);


            return Json(new { });
        }

        [HttpPost]
        public IActionResult ChangeVehicleType(int id, short passengersCount, short suitcasesCount)
        {
            dynamic? getVehicleTypes = null;
            List<DistanceAmountListVM>? distanceAmountListVMs = new List<DistanceAmountListVM>();
            VehicleAmounts? vehicleAmountsDetails = null;
            if (id == 0)
            {
                getVehicleTypes = _context.VehicleTypes.Where(x => x.Status).ToList();
                foreach (var getVehicleType in getVehicleTypes)
                {
                    vehicleAmountsDetails = HttpContext.GetObjectsession<VehicleAmounts>($"{getVehicleType.TypeName.Replace(" ", "").ToLower()}-result");
                    distanceAmountListVMs.Add(new DistanceAmountListVM()
                    {
                        DistanceAmount = vehicleAmountsDetails?.DistanceAmount,
                        VehicleTypeId = getVehicleType.Id,
                        TypeName = getVehicleType.TypeName,
                        IsActive = vehicleAmountsDetails.IsActive,
                        PassengersCount = passengersCount,
                        SuitCasesCount = suitcasesCount
                    });
                }
            }
            else
            {
                getVehicleTypes = _context.VehicleTypes.Where(x => x.Id == id).FirstOrDefault();
                vehicleAmountsDetails = HttpContext.GetObjectsession<VehicleAmounts>($"{getVehicleTypes?.TypeName.Replace(" ", "").ToLower()}-result");
                distanceAmountListVMs.Add(new DistanceAmountListVM()
                {
                    DistanceAmount = vehicleAmountsDetails?.DistanceAmount,
                    VehicleTypeId = getVehicleTypes?.Id,
                    TypeName = getVehicleTypes?.TypeName,
                    IsActive = vehicleAmountsDetails.IsActive,
                    PassengersCount = passengersCount,
                    SuitCasesCount = suitcasesCount
                });
            }

            return Json(new { distanceAmountListVMs });
        }

        [HttpPost]
        public IActionResult CalculateBonusTotalAmount(decimal percentage)
        {
            if (percentage <= 0)
            {
                return Json(new { wrongPercentage = true });
            }



            VehicleAmounts? vehicleAmounts = HttpContext?.GetObjectsession<VehicleAmounts>("activeVehicleAmountSession");


            string calculateTotalAmountForPercentage = (Convert.ToDecimal(vehicleAmounts?.DistanceAmount) * (percentage / 100)).ToString("F2");

            decimal calculatedTotalAmount = Convert.ToDecimal(calculateTotalAmountForPercentage) + Convert.ToDecimal(vehicleAmounts?.DistanceAmount);

            var calculatedVehicleAmounts = new VehicleAmounts
            {
                DistanceAmount = vehicleAmounts?.DistanceAmount,
                Graduity = calculateTotalAmountForPercentage,
                TotalAmount = calculatedTotalAmount.ToString(),
                IsActive = vehicleAmounts.IsActive,
                VehicleTypeId = vehicleAmounts.VehicleTypeId
            };


            HttpContext?.Session?.Remove("activeVehicleAmountSession");

            HttpContext?.SetObjectsession("activeVehicleAmountSession", calculatedVehicleAmounts);


            return Json(new { calculatedVehicleAmounts, calculateTotalAmountForPercentage });

        }

        [HttpPost]
        public IActionResult CalculateBonusGradiutyTotalAmount(int gradiuty)
        {
            if (gradiuty <= 0)
            {
                return Json(new { wrongPercentage = true });
            }



            VehicleAmounts? vehicleAmounts = HttpContext?.GetObjectsession<VehicleAmounts>("activeVehicleAmountSession");


            string calculateTotalAmountForGradiuty = (Convert.ToDecimal(vehicleAmounts?.DistanceAmount) + gradiuty).ToString("F2");

            decimal calculatedTotalAmount = Convert.ToDecimal(calculateTotalAmountForGradiuty) + Convert.ToDecimal(vehicleAmounts?.DistanceAmount);

            var calculatedVehicleAmounts = new VehicleAmounts
            {
                DistanceAmount = vehicleAmounts?.DistanceAmount,
                Graduity = gradiuty.ToString(),
                TotalAmount = calculateTotalAmountForGradiuty,
                IsActive = vehicleAmounts.IsActive,
                VehicleTypeId = vehicleAmounts.VehicleTypeId
            };


            HttpContext?.Session?.Remove("activeVehicleAmountSession");

            HttpContext?.SetObjectsession("activeVehicleAmountSession", calculatedVehicleAmounts);


            return Json(new { calculatedVehicleAmounts, calculateTotalAmountForGradiuty });

        }

        [HttpPost]
        public IActionResult CalculateTotalAmountForCuponCode(string cuponkey)
        {
            if (String.IsNullOrWhiteSpace(cuponkey))
            {
                return Json(new { wrongCuponKey = true });
            }

            var cuponDetails = _context.Cupons.Where(c => c.NewCupon == cuponkey)
                                              .Select(x => new Cupon
                                              {
                                                  NewCupon = x.NewCupon,
                                                  Percentage = x.Percentage,
                                                  Amount = x.Amount,
                                                  UseCount = x.UseCount,
                                              }).FirstOrDefault();

            if (cuponDetails == null)
            {
                return Json(new { notFound = true });
            }

            var contactDetails = HttpContext.GetObjectsession<ContactDetailsVM>("contactDetails");

            VehicleAmounts? vehicleAmounts = HttpContext?.GetObjectsession<VehicleAmounts>("activeVehicleAmountSession");



            var getUserUsedCupons = _context.UserUsedCupons.Where(x => x.Email == contactDetails.Email && x.CuponKey == cuponkey)
                                                          .Select(x => new UserUsedCupon { CuponKey = x.CuponKey, Email = x.Email, UniqueKey = x.UniqueKey }).ToList();

            string activeUniquekey = HttpContext.GetSessionString("activeUniqueKey");


            UserCuponVM userCuponVM = null;


            if (getUserUsedCupons.Count == 0)
            {
                userCuponVM = UseCupon(cuponDetails, vehicleAmounts, contactDetails, activeUniquekey);
            }
            else
            {
                if (getUserUsedCupons.Count < cuponDetails.UseCount)
                {
                    foreach (var getUserUsedCupon in getUserUsedCupons)
                    {
                        if (getUserUsedCupon.CuponKey.Equals(cuponDetails.NewCupon) && activeUniquekey.Equals(getUserUsedCupon.UniqueKey))
                        {

                            return Json(new { usedCupon = true });
                        }
                    }
                }
                else
                {
                    return Json(new { expiredCupon = true });
                }

                userCuponVM = UseCupon(cuponDetails, vehicleAmounts, contactDetails, activeUniquekey);
            }

            return Json(new { userCuponVM, status = Ok() });

        }

        public UserCuponVM UseCupon(Cupon? cuponDetails, VehicleAmounts? vehicleAmounts, ContactDetailsVM contactDetails, string uniqueKey)
        {
            decimal discountAmount = 0;
            string? DiscountTotalAmount = null;
            string? discountType = null;
            if (cuponDetails?.Percentage != 0)
            {
                discountAmount = (Convert.ToDecimal(vehicleAmounts?.TotalAmount) * cuponDetails.Percentage / 100);
                DiscountTotalAmount = (Convert.ToDecimal(vehicleAmounts?.TotalAmount) - discountAmount).ToString("F2");

                discountType = $"{cuponDetails.Percentage}%";
            }

            if (cuponDetails?.Amount != 0)
            {
                DiscountTotalAmount = (Convert.ToDecimal(vehicleAmounts?.TotalAmount) - cuponDetails.Amount).ToString("F2");
                discountType = $"${cuponDetails.Amount}";
            }

            var calculatedVehicleAmounts = new VehicleAmounts
            {
                DistanceAmount = vehicleAmounts?.DistanceAmount,
                Graduity = vehicleAmounts?.Graduity,
                TotalAmount = DiscountTotalAmount,
                IsActive = vehicleAmounts.IsActive,
                VehicleTypeId = vehicleAmounts.VehicleTypeId
            };



            HttpContext?.Session?.Remove("activeVehicleAmountSession");

            HttpContext?.SetObjectsession("activeVehicleAmountSession", calculatedVehicleAmounts);

            UserCuponVM userCuponVM = new UserCuponVM()
            {
                DiscountType = discountType,
                VehicleAmounts = calculatedVehicleAmounts
            };

            UserUsedCupon userUsedCupon = new UserUsedCupon
            {
                CuponKey = cuponDetails.NewCupon,
                Email = contactDetails.Email,
                IsUsed = true,
                UniqueKey = uniqueKey
            };
            _context.UserUsedCupons.Add(userUsedCupon);
            _context.SaveChanges();


            return userCuponVM;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}