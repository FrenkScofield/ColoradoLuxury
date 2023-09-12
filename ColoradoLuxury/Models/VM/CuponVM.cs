namespace ColoradoLuxury.Models.VM
{
    public class CuponVM
    {
        public int Id { get; set; }
        public string? NewCupon { get; set; }
        public decimal Percentage { get; set; }
        public decimal Amount { get; set; }

        public string? CuponCode { get; set; }

        public bool Status { get; set; }

    }
}
