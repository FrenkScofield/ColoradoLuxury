namespace ColoradoLuxury.Models.BLL
{
    public class CustomerTravelType : AbstractEntity
    {
        public string Name { get; set; }

        public ICollection<RideDetail> RideDetails { get; set; }
    }
}
