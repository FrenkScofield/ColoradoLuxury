﻿namespace ColoradoLuxury.Models.VM
{
    public class VehicleAmounts
    {
        public string? DistanceAmount { get; set; }
        public string? Graduity { get; set; }
        public string? TotalAmount { get; set; }

        public string CuponValue { get; set; }
        public string? ResultTotalAmount { get; set; }


        public bool IsActive { get; set; }

        public int VehicleTypeId { get; set; }

    }
}
