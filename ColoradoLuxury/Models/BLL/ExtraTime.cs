namespace ColoradoLuxury.Models.BLL
{
    public class ExtraTime:AbstractEntity
    {
        public string? Time { get; set; }
        public ICollection<RideDetail> RideDetails { get; set; }

    }
}
