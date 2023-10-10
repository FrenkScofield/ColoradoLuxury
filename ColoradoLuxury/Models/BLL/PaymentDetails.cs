namespace ColoradoLuxury.Models.BLL
{
    public class PaymentDetails : AbstractEntity
    {
        public string? DistanceAmount { get; set; }
        public string? GradiutyAmount { get; set; }

        public string? TotalAmount { get; set; }

        public string? UsedCupon { get; set; }

        public string? DiscountCuponAmount { get; set; }

        public string TransactionId { get; set; }

        public int UserInfoId { get; set; }

        public UserInfo UserInfo { get; set; }

    }
}
