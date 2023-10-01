namespace ColoradoLuxury.Models.BLL
{
    public class DefineMinimumAmountAndDistanceSize:AbstractEntity
    {
        public decimal? MinimumMile { get; set; }
        public Int32? MinimumDuration { get; set; }
        public decimal? MinimumBookingvalueForDistance { get; set; }
        public decimal? MinimumBookingvalueForHourly { get; set; }

    }
}
