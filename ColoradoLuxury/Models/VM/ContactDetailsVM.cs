namespace ColoradoLuxury.Models.VM
{
    public class ContactDetailsVM
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AdditionalContactDetailNote { get; set; }



        public string? CompanyRegisteredname { get; set; }
        public string? TaxNumber { get; set; }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public int CountryId { get; set; }


        public int AirlineId { get; set; }
        public string? FlightNumber { get; set; }


    }
}
