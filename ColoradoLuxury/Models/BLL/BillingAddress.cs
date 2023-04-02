using ColoradoLuxury.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class BillingAddress : CoreEntity
    {
        [StringLength(150)]
        public string CompanyRegisteredName { get; set; }

        [StringLength(50)]
        public string TaxNumber { get; set; }

        [Required, StringLength(100)]
        public string Street { get; set; }

        [StringLength(100)]
        public string StreetNumber { get; set; }

        [Required, StringLength(50)]
        public string City { get; set; }

        [Required, StringLength(50)]
        public string State { get; set; }

        [Required, StringLength(50)]
        public string PostalCode { get; set; }

        [Required, StringLength(50)]
        public string Country { get; set; }

    }
}
