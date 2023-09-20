namespace ColoradoLuxury.Models.BLL
{
    public class UserInfo:AbstractEntity
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public int? BillingAddressId { get; set; }
        public int? ArrivalAirlineInfoId { get; set; }
        public int? ForDriverbettingId { get; set; }

        public BillingAddress BillingAddress { get; set; }
        public ArrivalAirlineInfo ArrivalAirlineInfo { get; set; }
        public ForDriverBetting ForDriverBetting { get; set; }




    }
}
