﻿namespace ColoradoLuxury.Models.BLL
{
    public class TransferType:AbstractEntity
    {
        public string? Name { get; set; }
        public ICollection<RideDetail> RideDetails { get; set; }

    }
}
