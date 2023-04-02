using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class ExtraTime:CoreEntity
    {
        [Required, StringLength(20)]
        public string Hours { get; set; }
    }
}
