using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace SalesWeb.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void SetName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public void RemoveSeller(Seller seller)
        {
            Sellers.Remove(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
