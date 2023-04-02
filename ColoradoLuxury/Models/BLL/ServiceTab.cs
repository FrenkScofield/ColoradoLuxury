using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class ServiceTab:CoreEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}
