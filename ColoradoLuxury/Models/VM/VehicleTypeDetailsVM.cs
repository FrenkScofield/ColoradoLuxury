﻿namespace ColoradoLuxury.Models.VM
{
    public class VehicleTypeDetailsVM
    {
        public int Id { get; set; }
        public string? TypeName { get; set; }
        public string? Hourly { get; set; }

        public string? PerMile { get; set; }

        public bool IsActive { get; set; }

    }
}
