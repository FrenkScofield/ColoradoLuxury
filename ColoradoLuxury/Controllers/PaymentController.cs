using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Microsoft.CodeAnalysis.CSharp.Syntax;
//using ColoradoLuxury.Models.VM;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using ColoradoLuxury.Models.VM;
using Microsoft.AspNetCore.Identity.UI.Services;
using ColoradoLuxury.Extensions;
using ColoradoLuxury.Services;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Enums;
using ColoradoLuxury.Models.DAL;

namespace ColoradoLuxury.Controllers
{
    public class PaymentController : Controller
    {
        public string? SessionId { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailsender;
        private readonly IViewRenderService _viewRenderService;
        private readonly ColoradoContext _context;

        public PaymentController(IConfiguration configuration, IEmailSender emailsender, IViewRenderService viewRenderService, ColoradoContext context)
        {
            _configuration = configuration;
            _emailsender = emailsender;
            _viewRenderService = viewRenderService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> SuccessAsync()
        {
            //Insert 
            //Get All Informations
            var rideDetails = HttpContext.Session.GetObjectsession<RideDetailsVM>("rideDetails");
            var vehicleDetails = HttpContext.Session.GetObjectsession<ChooseVehicleVM>("vehicleDetails");
            var contactDetails = HttpContext.Session.GetObjectsession<ContactDetailsVM>("contactDetails");
            BillingAddress billingAddress = null;
            ArrivalAirlineInfo airlineInfo = null;
            //using (var transaction = _context.Database.BeginTransaction())
            //{
            try
            {

                RideDetail rideDetail = new RideDetail()
                {
                    PickupDate = rideDetails.PickupDate,
                    PickupTime = rideDetails.PickupTime,
                    PickupLocation = rideDetails.PickupLocation,
                    DropOffLocation = rideDetails.DropOffLocation,
                    CustomerTravelTypeId = rideDetails.WayType ? (int)WayTypeEnum.Distance : (int)WayTypeEnum.Hourly,
                    DurationId = rideDetails.DurationInHours != null ? rideDetails.DurationInHours : null,
                    TransferTypeId = rideDetails.TransferTypeId != 0 ? rideDetails.TransferTypeId : 0
                };

                VehicleInfoDetails vehicleInfoDetails = new VehicleInfoDetails()
                {
                    PassengersCount = vehicleDetails.PassengersSelect,
                    SuitCasesCount = vehicleDetails.Suitcases,
                    VehicleTypeId = vehicleDetails.AllcarTpes,
                    ChildSeatCount = vehicleDetails.ChildNumber,
                    ChildSeatDescription = vehicleDetails.ChildAdditionalMessage,
                    RoofTopCargoBoxCount = vehicleDetails.RoofCargoBoxNumber,
                    RoofTopCargoBoxDescription = vehicleDetails.RoofCargoBoxAdditionalMessage
                };

                await _context.RideDetails.AddAsync(rideDetail);
                await _context.SaveChangesAsync();
                await _context.VehicleInfoDetails.AddAsync(vehicleInfoDetails);
                await _context.SaveChangesAsync();


                if (contactDetails.BillingAddressStatus)
                {
                    billingAddress = new BillingAddress()
                    {
                        Company = contactDetails.CompanyRegisteredname,
                        Tax = contactDetails.TaxNumber,
                        City = contactDetails.City,
                        Street = contactDetails.Street,
                        State = contactDetails.State,
                        Postal = contactDetails.PostalCode,
                        CountryId = contactDetails.CountryId,
                        Status = true
                    };
                    await _context.BillingAddress.AddAsync(billingAddress);
                    await _context.SaveChangesAsync();
                }

                if (contactDetails.AirLineStatus)
                {
                    airlineInfo = new ArrivalAirlineInfo()
                    {
                        Flight = contactDetails.FlightNumber,
                        AirlineId = contactDetails.AirlineId
                    };
                    await _context.ArrivalAirlineInfos.AddAsync(airlineInfo);
                    await _context.SaveChangesAsync();
                }

                UserInfo userInfo = new UserInfo()
                {
                    Firstname = contactDetails.Firstname,
                    Surname = contactDetails.Lastname,
                    Email = contactDetails.Email,
                    Phone = contactDetails.PhoneNumber,
                    Message = contactDetails.AdditionalContactDetailNote,
                    BillingAddressId = billingAddress != null ? billingAddress.Id : null,
                    ArrivalAirlineInfoId = airlineInfo != null ? airlineInfo.Id : null
                };
                await _context.UserInfos.AddAsync(userInfo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();

                throw;
            }
            //}


            FilledAllDatas datas = new FilledAllDatas()
            {
                CustomerTravelType = rideDetails.WayType ? WayTypeEnum.Distance.ToString() : WayTypeEnum.Hourly.ToString(),
                Firstname = contactDetails.Firstname,
                Lastname = contactDetails.Lastname,
                Email = contactDetails.Email,
                Phonenumber = contactDetails.PhoneNumber,
                ContactAdditionalNote = contactDetails.AdditionalContactDetailNote,
                CompanyName = contactDetails.CompanyRegisteredname,
                TaxNumber = contactDetails.TaxNumber,
                Street = contactDetails.Street,
                StreetNumber = contactDetails.StreetNumber,
                City = contactDetails.City,
                State = contactDetails.State,
                PostalCode = contactDetails.PostalCode,
                Country = contactDetails.CountryId != null ? _context.Countries.Where(c => c.Id == contactDetails.CountryId).FirstOrDefault()?.Name : null,
                Airline = contactDetails.AirlineId != null ? _context.AirLines.Where(a => a.Id == contactDetails.AirlineId).FirstOrDefault()?.Name : null,
                FlightNumber = contactDetails.FlightNumber,
                PassengersCount = vehicleDetails.PassengersSelect,
                SuitCasesCount = vehicleDetails.Suitcases,
                VehicleType = vehicleDetails.AllcarTpes != 0 ? _context.VehicleTypes.Where(v => v.Id == vehicleDetails.AllcarTpes).FirstOrDefault()?.TypeName : null,
                ChildSeatCount = vehicleDetails.ChildNumber,
                ChildSeatAdditionalNote = vehicleDetails.ChildAdditionalMessage,
                RoofCargoBoxCount = vehicleDetails.RoofCargoBoxNumber,
                RoofCargoBoxAdditionalNote = vehicleDetails.RoofCargoBoxAdditionalMessage,
                PickupDate = rideDetails.PickupDate.Date.ToShortDateString(),
                PickupTime = rideDetails.PickupTime,
                PickupLocation = rideDetails.PickupLocation,
                DropOffLocation = rideDetails.DropOffLocation,
                Duration = rideDetails.DurationInHours != null ? _context.Durations.Where(d => d.Id == rideDetails.DurationInHours).FirstOrDefault()?.Time : null,
                Mile = $"{Convert.ToDecimal(HttpContext.Session.GetString("mile"))} mi",
                DistanceTime = $"{HttpContext.Session.GetInt32("hours")}h {HttpContext.Session.GetInt32("minutes")}m",
                TotalPrice = HttpContext?.Session?.GetString("distanceAmount"),
                TransactionId = HttpContext?.Session?.GetString("TransactionId"),
                BillingAddressStatus = contactDetails.BillingAddressStatus,
                AirlineStatus = contactDetails.AirLineStatus
            };

            var result = await _viewRenderService.RenderToStringAsync("Checkout/Index", datas);

            try
            {
                await _emailsender.SendEmailAsync(contactDetails.Email, "Customer's all datas", result);
                await _emailsender.SendEmailAsync("eminah@code.edu.az", "Customer's all datas", result);
            }
            catch (Exception ex)
            {
                ExceptionExtension.Log(ex, _context);
                throw new Exception("There is problem on email provider!");
            }
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }

        [Route("payment")]
        public ActionResult Create()
        {
            var currency = "usd";
            var successUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Payment/Success";
            var cancelUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Payment/Cancel";

            StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeSettings:Secretkey");
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions> {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency= currency,
                            UnitAmountDecimal = Convert.ToDecimal(HttpContext?.Session?.GetString("totalAmount")) * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Travel",
                                Description = "Test"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);
            SessionId = session.Id;

            HttpContext?.Session?.SetString("TransactionId", SessionId);
            return Redirect(session.Url);
        }
    }
}

