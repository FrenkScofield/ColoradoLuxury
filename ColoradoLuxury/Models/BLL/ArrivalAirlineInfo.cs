namespace ColoradoLuxury.Models.BLL
{
    public class ArrivalAirlineInfo:AbstractEntity
    {
        public string? Flight { get; set; }
        public int? AirlineId { get; set; }

        public bool Status { get; set; }

        public AirLine AirLine { get; set; }

    }
}
