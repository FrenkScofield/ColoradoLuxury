namespace ColoradoLuxury.Models.BLL
{
    public class BillingAddress : AbstractEntity
    {
        public string? Company { get; set; }
        public string? Tax { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postal { get; set; }
        public int CountryId { get; set; }

        public bool Status { get; set; }
        public Country Country { get; set; }


    }
}
