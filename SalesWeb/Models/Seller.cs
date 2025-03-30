namespace SalesWeb.Models
{
    public class Seller
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public double BaseSalary { get; private set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; private set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void SetName(string name)
        {
            if(!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
        }

        public void SetEmail(string email)
        {
            if(!string.IsNullOrWhiteSpace(email))
            {
                Email = email;
            }
        }

        public void AddSales(SalesRecord sales)
        {
            Sales.Add(sales);
        }

        public void RemoveSales(SalesRecord sales)
        {
            Sales.Remove(sales);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales
                .Where(sales => sales.Date >= initial && sales.Date <= final)
                .Sum(sales => sales.Amount);
        }
    }
}
