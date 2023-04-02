using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class TransferType:CoreEntity
    {
        [Required, StringLength(100)]
        public string Type { get; set; }
    }
}
