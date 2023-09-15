namespace ColoradoLuxury.Models.BLL
{
    public class Cupon : AbstractEntity
    {
        public string? NewCupon { get; set; }
        public decimal Percentage { get; set; }
        public decimal Amount { get; set; }

        public string? CuponCode { get; set; }

        public DateTime? CouponDeatline { get; set; }


        private bool status;

        public bool Status
        {
            get { return status; }
            set { status = true; }
        }


    }
}
