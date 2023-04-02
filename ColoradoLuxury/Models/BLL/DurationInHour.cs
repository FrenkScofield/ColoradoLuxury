using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class DurationInHour : CoreEntity
    {
        [Required, StringLength(20)]
        public string Hours { get; set; }
    }
}
