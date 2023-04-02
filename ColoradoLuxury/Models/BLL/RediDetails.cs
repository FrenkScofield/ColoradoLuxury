using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class RediDetails : CoreEntity
    {
        [Required]
        public DateTime PickupDate { get; set; }

        [Required, StringLength(50)]
        public string PickupTime { get; set; }

        public string PickupLocation { get; set; }

        public string DropOffLocationn { get; set; }




        //Foreign keys
        public int ExtraTimeId { get; set; }
        public ExtraTime ExtraTimes { get; set; }

        public int DurationInHoursId { get; set; }
        public DurationInHour DurationInHours { get; set; }

        public string TransferTypeId { get; set; }
        public TransferType TransferType { get; set; }

    }
}
