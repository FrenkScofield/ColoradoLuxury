using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class VehicleType : CoreEntity
    {
        [Required, StringLength(50)]
        public string Type { get; set; }
    }
}
