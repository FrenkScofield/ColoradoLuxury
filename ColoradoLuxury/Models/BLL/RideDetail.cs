namespace ColoradoLuxury.Models.BLL
{
    public class RideDetail : AbstractEntity
    {
        public DateTime PickupDate { get; set; }
        public string? PickupTime { get; set; }
        public string? PickupLocation { get; set; }
        public string? DropOffLocation { get; set; }

        public DateTime EndDate { get; set; }
        public string? EndPickupTime { get; set; }
        public int CustomerTravelTypeId { get; set; }
        public int? TransferTypeId { get; set; }
        public int? DurationId { get; set; }

        public CustomerTravelType CustomerTravelType { get; set; }
        public TransferType TransferType { get; set; }

        public Duration Duration { get; set; }




    }
}
