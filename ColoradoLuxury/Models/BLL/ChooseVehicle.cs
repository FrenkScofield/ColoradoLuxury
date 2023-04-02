using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class ChooseVehicle : CoreEntity
    {
        [Required]
        public string Passengers { get; set; }
        [Required]
        public string Suitcases { get; set; }
        [Required]
        public int ChildSeatCount { get; set; }
        [Required]
        public string RooftopCargoBoxCount { get; set; }
        public string ChildSeatNote { get; set; }
        public string RooftopCargoBoxNote { get; set; }

        //Foreign keys

        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }


    }
}
