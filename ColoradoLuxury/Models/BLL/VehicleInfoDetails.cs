namespace ColoradoLuxury.Models.BLL
{
    public class VehicleInfoDetails:AbstractEntity
    {
        public short PassengersCount { get; set; }
        public short SuitCasesCount { get; set; }

        public int VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }

        public short ChildSeatCount { get; set; }
        public string ChildSeatDescription { get; set; }

        public short RoofTopCargoBoxCount { get; set; }
        public string RoofTopCargoBoxDescription { get; set; }

    }
}
