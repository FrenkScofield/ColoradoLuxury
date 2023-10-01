namespace ColoradoLuxury.Models.BLL
{
    public class Vehicle:AbstractEntity
    {
        public string? Name { get; set; }
        public string? Model { get; set; }
        public int Engine { get; set; }
        public string? Fuel { get; set; }

        public string? Color { get; set; }

        public int VehicleTypeId { get; set; }

        public VehicleType? VehicleType { get; set; }
    }
}
