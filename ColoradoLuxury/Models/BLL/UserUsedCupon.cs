namespace ColoradoLuxury.Models.BLL
{
    public class UserUsedCupon : AbstractEntity
    {
        public string? CuponKey { get; set; }

        public string? Email { get; set; }

        public string? UniqueKey { get; set; }

        public bool IsUsed { get; set; }
        
    }
}
