namespace ColoradoLuxury.Models.BLL
{
    public class Duration:AbstractEntity
    {
        public string? Time { get; set; }
        public ICollection<RideDetail> RideDetails { get; set; }

    }
}
