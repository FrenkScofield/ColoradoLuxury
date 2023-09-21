namespace ColoradoLuxury.Models.VM
{
    public class RideDetailsVM
    {
        public bool WayType { get; set; }
        public DateTime PickupDate { get; set; }

        public string? PickupTime { get; set; }
        public string? PickupLocation { get; set; }
        public string? DropOffLocation { get; set; }
        public int TransferTypeId { get; set; }

        public int? DurationInHours { get; set; }


        public string GetPickupDateAndTime()
        {
            return String.Concat(PickupDate.Date.ToShortDateString(),", ", PickupTime);
        }

    }
}
