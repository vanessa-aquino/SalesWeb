using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using SalesWeb.Services;
using SalesWeb.Models;
using SalesWeb.Data;
using System.Xml.Linq;
using SalesWeb.Services.Exceptions;

namespace SalesWeb.Tests.Services
{
    public class SellerServiceTests
    {

        private SalesWebContext GetInMemoryDbContext()
        {
            // Configura um banco de dados em mémoria pra ser usado nos teste:
            var options = new DbContextOptionsBuilder<SalesWebContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Banco fake, e cada teste cria seu próprio banco isolado
                .EnableSensitiveDataLogging()
                .Options;

            var department = new Department { Id = 1, Name = "TI" };

            var context = new SalesWebContext(options);

            context.Department.Add(department);
            context.Seller.AddRange
            (
                new Seller { Id = 1, Name = "Primeiro Nome", Email = "teste1@email.com", BirthDate = new DateTime(1999, 1, 1), BaseSalary = 2000.00, DepartmentId = 1, Department = department},
                new Seller { Id = 2, Name = "Segundo Nome", Email = "teste2@email.com", BirthDate = new DateTime(1998, 2, 1), BaseSalary = 1000.00, DepartmentId = 1, Department = department }
            );

            context.SaveChanges();
            return context;
        }

        [Fact] // Para marcar que é um teste
        public async Task FindAllAsync_ReturnSellerList()
        {
            var context = GetInMemoryDbContext();
            var service = new SellerService(context); // Cria o SellerService com o novo banco de dados criado
            var result = await service.FindAllAsync(); // Executa o método que deve retornar todos os vendedores

            Assert.NotNull(result);
            Assert.IsType<List<Seller>>(result);
            Assert.Equal(2, result.Count); // verifica se o método retornou dois vendedores, como esperado
        }

        [Fact]
        public async Task FindByIdAsync_ValidId_ReturnsSeller()
        {
            var context = GetInMemoryDbContext();
            var service = new SellerService(context);
            var result = await service.FindByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Primeiro Nome", result.Name);
        }

        [Fact]
        public async Task FindByIdAsync_InvalidId_ReturnsNull()
        {
            var context = GetInMemoryDbContext();
            var service = new SellerService(context);
            var result = await service.FindByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task InsertAsync_AddSeller()
        {
            var context = GetInMemoryDbContext();
            var service = new SellerService(context);

            var newSeller = new Seller { Id = 3, Name = "Insert Nome", Email = "teste-insert@email.com", BirthDate = new DateTime(1998, 2, 1), BaseSalary = 1000.00 };
            await service.InsertAsync(newSeller);

            var seller = await context.Seller.FindAsync(3);

            Assert.NotNull(seller);
            Assert.Equal("Insert Nome", seller.Name);
        }

        [Fact]
        public async Task RemoveAsync_RemoveSeller()
        {
            var context = GetInMemoryDbContext();
            var service = new SellerService(context);

            await service.RemoveAsync(2);

            var removeSeller = await context.Seller.FindAsync(2);

            Assert.Null(removeSeller);
        }

        [Fact]
        public async Task RemoveAsync_InvalidId_ThrowsNotFoundException()
        {
            var context = GetInMemoryDbContext();
            var service = new SellerService(context);

            await Assert.ThrowsAsync <NotFoundException>(() => service.RemoveAsync(999));
        }

        [Fact]
        public async Task UpdateAsync_ValidId_ChangesData()
        {
            var context = GetInMemoryDbContext();
            var service = new SellerService(context);

            var seller = await context.Seller.FindAsync(1);
            seller.Name = "Atualizado";
            
            await service.UpdateAsync(seller);

            var update = await context.Seller.FindAsync(1);

            Assert.NotNull(seller);
            Assert.Equal("Atualizado", seller.Name);
        }


        [Fact]
        public async Task UpdateAsync_InvalidId_ThrowsNotFoundException()
        {
            var context = GetInMemoryDbContext();
            var service = new SellerService(context);

            var seller = new Seller { Id = 999, Name = "X", Email = "x@email.com", DepartmentId = 1 };
            await Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(seller));
        }
    }
}

