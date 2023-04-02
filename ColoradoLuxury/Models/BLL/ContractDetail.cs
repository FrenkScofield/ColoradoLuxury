using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class ContractDetail : CoreEntity
    {
        [Required, StringLength(50)]
        public string Firstname { get; set; }

        [Required, StringLength(50)]
        public string Lastname { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string PhonePrefix { get; set; }

        [Required, StringLength(50)]
        public string Phonenumber { get; set; }

        [StringLength(300)]
        public string Comments { get; set; }

        //Foreign keys

        public int BillingAddressId { get; set; }
        public BillingAddress BillingAddress { get; set; }

        public int ArrivalAirlineInfoId { get; set; }
        public ArrivalAirlineInfo ArrivalAirlineInfo { get; set; }
    }
}
