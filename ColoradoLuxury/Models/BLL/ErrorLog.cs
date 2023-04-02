using ColoradoLuxury.Abstract;

namespace ColoradoLuxury.Models.BLL
{
    public class ErrorLog : CoreEntity
    {
        public string ErrorMessage { get; set; }

        public DateTime LogDate { get; set; } = DateTime.Now;

        public string Url { get; set; }
        public int StepNumber { get; set; }
    }
}
