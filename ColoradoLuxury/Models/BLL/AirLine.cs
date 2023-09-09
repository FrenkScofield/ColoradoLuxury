namespace ColoradoLuxury.Models.BLL
{
    public class AirLine : AbstractEntity
    {
        public string? Name { get; set; }
        public ICollection<ArrivalAirlineInfo> ArrivalAirlineInfos { get; set; }
    }
}
