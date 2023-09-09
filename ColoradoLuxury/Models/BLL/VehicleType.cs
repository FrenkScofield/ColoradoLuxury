namespace ColoradoLuxury.Models.BLL
{
    public class VehicleType:AbstractEntity
    {
        public string? TypeName { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
