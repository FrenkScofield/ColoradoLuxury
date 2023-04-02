using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ColoradoLuxury.Models.BLL
{
    public class User: IdentityUser
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }

        public string MiddleName { get; set; }

        public string Address { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
