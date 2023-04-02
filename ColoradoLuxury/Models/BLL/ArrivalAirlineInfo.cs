using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class ArrivalAirlineInfo : CoreEntity
    {
        [Required, StringLength(50)]
        public string FlightNumber { get; set; }


        //Foreign keys
        public int AirLineId { get; set; }

        public AirLine AirLine { get; set; }
    }
}
