using ColoradoLuxury.Models.BLL;

namespace ColoradoLuxury.Models.VM
{
    public class UserRecordsVM
    {
        public RideDetail? RideDetail { get; set; }
        public UserInfo? UserInfo { get; set; }
        public VehicleInfoDetails? VehicleInfoDetails { get; set; }
        public ArrivalAirlineInfo? ArrivalAirlineInfo { get; set; }
        public BillingAddress? BillingAddress { get; set; }

        public PaymentDetails? PaymentDetails { get; set; }




    }
}
