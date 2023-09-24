namespace ColoradoLuxury.Models.BLL
{
    public class VehicleType : AbstractEntity
    {
        public string? TypeName { get; set; }

        public decimal PerMile { get; set; }
        public decimal Hourly { get; set; }
        public bool Status { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
