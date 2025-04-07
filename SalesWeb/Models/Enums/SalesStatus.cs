using System.ComponentModel.DataAnnotations;

namespace SalesWeb.Models.Enums
{
    public enum SalesStatus
    {
        [Display(Name = "Pendente")]
        Pending,
        [Display(Name = "Finalizada")]
        Billed,
        [Display(Name = "Cancelada")]
        Canceled
    }
}
