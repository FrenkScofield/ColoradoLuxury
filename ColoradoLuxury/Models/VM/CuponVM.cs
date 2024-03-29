﻿namespace ColoradoLuxury.Models.VM
{
    public class CuponVM
    {
        public int Id { get; set; }
        public string? NewCupon { get; set; }
        public decimal Percentage { get; set; }
        public decimal Amount { get; set; }

        public DateTime? CouponDeatline { get; set; }

        public byte? UsableCount { get; set; }

        public int Status { get; set; }

    }
}
