namespace ColoradoLuxury.Models.BLL
{
    public class BillingAddress : AbstractEntity
    {
        public string? Company { get; set; }
        public int Tax { get; set; }
        public int Street { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Postal { get; set; }
        public int CountryId { get; set; }

        public Country Country { get; set; }


    }
}
