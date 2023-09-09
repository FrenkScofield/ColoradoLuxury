namespace ColoradoLuxury.Models.BLL
{
    public abstract class AbstractEntity
    {
        public int Id { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime EditDate { get; set; }

        public string? ClientIpAddress { get; set; }

        public AbstractEntity()
        {
            RegDate = DateTime.Now;
        }
    }
}
