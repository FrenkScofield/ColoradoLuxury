namespace ColoradoLuxury.Models.VM
{
    public class DistanceAmountListVM
    {
        public string? TypeName { get; set; }
        public string? DistanceAmount { get; set; }
        public int VehicleTypeId { get; set; }

        public short PassengersCount { get; set; }
        public short SuitCasesCount { get; set; }


        public bool IsActive { get; set; }
    }
}
