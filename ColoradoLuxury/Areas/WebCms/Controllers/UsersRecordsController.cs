using ColoradoLuxury.Attributes;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;
using Microsoft.AspNetCore.Mvc;


namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class UsersRecordsController : AuthController
    {
        private readonly ColoradoContext _context;
        public UsersRecordsController(ColoradoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userInfos = _context.UserInfos.Select(x => new UserInfo
            {
                Firstname = x.Firstname,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                Id = x.Id,
                VehicleInfoDetails= x.VehicleInfoDetails,
                VehicleInfoDetailsId = x.VehicleInfoDetailsId,
                RegDate = x.RegDate,
            }).OrderByDescending(x => x.Id).ToList();
            return View(userInfos);
        }

        public IActionResult OrderRecord(int id)
        {
            if (id <= 0)
                return NotFound();

            var userInfos = _context.UserInfos.Where(x => x.Id == id).Select(x => new UserInfo
            {
                Firstname = x.Firstname,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                Id = x.Id,
                VehicleInfoDetails = x.VehicleInfoDetails,
                VehicleInfoDetailsId = x.VehicleInfoDetailsId,
                RideDetailId = x.RideDetailId,
                ArrivalAirlineInfoId = x.ArrivalAirlineInfoId
            }).AsQueryable();


            if (userInfos == null)
                return NotFound();

            var rideDetails = _context.RideDetails.Where(x => x.Id == userInfos.FirstOrDefault().RideDetailId).Select(x => new RideDetail
            {
                PickupLocation = x.PickupLocation,
                DropOffLocation = x.DropOffLocation,
                PickupDate = x.PickupDate,
                PickupTime = x.PickupTime,
                TransferType = x.TransferType,
                Duration = x.Duration,
                DurationId = x.DurationId,
            }).AsQueryable();



            var vehicleType = _context.VehicleInfoDetails.Where(x => x.Id == userInfos.FirstOrDefault().VehicleInfoDetailsId).Select(x => new VehicleInfoDetails
            {
                PassengersCount = x.PassengersCount,
                SuitCasesCount = x.SuitCasesCount,
                ChildSeatCount = x.ChildSeatCount,
                ChildSeatDescription = x.ChildSeatDescription,
                RoofTopCargoBoxCount = x.RoofTopCargoBoxCount,
                RoofTopCargoBoxDescription = x.RoofTopCargoBoxDescription
            }).AsQueryable();

            var airLine = _context.ArrivalAirlineInfos.Where(x => x.Id == userInfos.FirstOrDefault().ArrivalAirlineInfoId).Select(x => new ArrivalAirlineInfo
            {
                Flight = x.Flight,
                AirLine = x.AirLine,
                AirlineId = x.AirlineId
            }).AsQueryable();

            var billingAddress = _context.BillingAddress.Where(x => x.Id == userInfos.FirstOrDefault().BillingAddressId).Select(x => new BillingAddress
            {
                Company = x.Company,
                Street = x.Street,
                City = x.City,
                State = x.State,
                Postal = x.Postal,
                Country = x.Country,
                CountryId = x.CountryId,
                Tax = x.Tax
            }).AsQueryable();

            var paymentDetails = _context.PaymentDetails.Where(x => x.Id == userInfos.FirstOrDefault().Id).Select(x => new PaymentDetails
            {
                DistanceAmount = x.DistanceAmount,
                DiscountCuponAmount = x.DiscountCuponAmount,
                GradiutyAmount = x.GradiutyAmount,
                TotalAmount = x.TotalAmount,
                UsedCupon = x.UsedCupon
            }).AsQueryable();

            UserRecordsVM userRecordsVM = new UserRecordsVM()
            {
                UserInfo = userInfos.FirstOrDefault(),
                RideDetail = rideDetails.FirstOrDefault(),
                VehicleInfoDetails = vehicleType.FirstOrDefault(),
                ArrivalAirlineInfo = airLine.FirstOrDefault(),
                BillingAddress = billingAddress.FirstOrDefault(),
                PaymentDetails = paymentDetails.FirstOrDefault(),
            };


            return View(userRecordsVM);
        }
    }
}
