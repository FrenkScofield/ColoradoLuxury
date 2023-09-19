namespace ColoradoLuxury.Models.BLL
{
    public class Country:AbstractEntity
    {
        public string? Name { get; set; }
        public ICollection<BillingAddress> BillingAddresses { get; set; }
    }
}
