namespace ColoradoLuxury.Models.BLL
{
    public class Country:AbstractEntity
    {
        public int Name { get; set; }
        public ICollection<BillingAddress> BillingAddresses { get; set; }
    }
}
