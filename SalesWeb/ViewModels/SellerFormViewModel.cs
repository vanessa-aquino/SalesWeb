using SalesWeb.Models;

namespace SalesWeb.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
        
    }
}
