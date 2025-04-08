using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SalesWeb.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Nome completo")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de nascimento")]
        public DateTime BirthDate { get; set; }
    }
}
