using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;
using SalesWeb.Models.Enums;

namespace SalesWeb.Tests.Utils
{
    public static class TestDbContextFactory
    {
        public static SalesWebContext GetInMemoryDbContext()
        {
            // Configura um banco de dados em mémoria pra ser usado nos teste:
            var options = new DbContextOptionsBuilder<SalesWebContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Banco fake, e cada teste cria seu próprio banco isolado
                .EnableSensitiveDataLogging()
                .Options;

            var department = new Department { Id = 1, Name = "TI" };
            var context = new SalesWebContext(options);

            context.Department.Add(department);

            var seller1 = new Seller
            {
                Id = 1,
                Name = "Primeiro Nome",
                Email = "teste1@email.com",
                BirthDate = new DateTime(1999, 1, 1),
                BaseSalary = 2000.00,
                DepartmentId = 1,
                Department = department
            };
            var seller2 = new Seller
            {
                Id = 2,
                Name = "Segundo Nome",
                Email = "teste2@email.com",
                BirthDate = new DateTime(1998, 2, 1),
                BaseSalary = 1000.00,
                DepartmentId = 1,
                Department = department
            };

            context.Seller.AddRange(seller1, seller2);

            context.SalesRecords.AddRange
                (
                    new SalesRecord
                    {
                        Id = 1,
                        Date = DateTime.Now.AddDays(-10),
                        Amount = 1500.00,
                        Status = SalesStatus.Billed,
                        Seller = seller1
                    },
                    new SalesRecord
                    {
                        Id = 2,
                        Date = DateTime.Now.AddDays(-5),
                        Amount = 2500.00,
                        Status = SalesStatus.Pending,
                        Seller = seller2
                    }
                );

            context.SaveChanges();
            return context;
        }
    }
}
