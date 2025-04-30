using SalesWeb.Services;
using SalesWeb.Models;
using SalesWeb.Services.Exceptions;
using SalesWeb.Tests.Common;

namespace SalesWeb.Tests.Services
{
    public class SellerServiceTests : BaseTest
    {

        [Fact] // Para marcar que é um teste
        public async Task FindAllAsync_ReturnSellerList()
        {
            var result = await SellerService.FindAllAsync(); // Executa o método que deve retornar todos os vendedores

            Assert.NotNull(result);
            Assert.IsType<List<Seller>>(result);
            Assert.Equal(2, result.Count); // verifica se o método retornou dois vendedores, como esperado
        }

        [Fact]
        public async Task FindByIdAsync_ValidId_ReturnsSeller()
        {
            var result = await SellerService.FindByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Primeiro Nome", result.Name);
        }

        [Fact]
        public async Task FindByIdAsync_InvalidId_ReturnsNull()
        {
            var result = await SellerService.FindByIdAsync(999);
            Assert.Null(result);
        }

        [Fact]
        public async Task InsertAsync_AddSeller()
        {

            var newSeller = new Seller { Id = 3, Name = "Insert Nome", Email = "teste-insert@email.com", BirthDate = new DateTime(1998, 2, 1), BaseSalary = 1000.00 };
            await SellerService.InsertAsync(newSeller);
            var seller = await Context.Seller.FindAsync(3);

            Assert.NotNull(seller);
            Assert.Equal("Insert Nome", seller.Name);
        }

        [Fact]
        public async Task RemoveAsync_RemoveSeller()
        {

            await SellerService.RemoveAsync(2);
            var removeSeller = await Context.Seller.FindAsync(2);

            Assert.Null(removeSeller);
        }

        [Fact]
        public async Task UpdateAsync_ValidId_ChangesData()
        {

            var seller = await Context.Seller.FindAsync(1);
            seller.Name = "Atualizado";
            
            await SellerService.UpdateAsync(seller);
            var update = await Context.Seller.FindAsync(1);

            Assert.NotNull(seller);
            Assert.Equal("Atualizado", seller.Name);
        }


        [Fact]
        public async Task UpdateAsync_InvalidId_ThrowsNotFoundException()
        {
            var seller = new Seller { Id = 999, Name = "X", Email = "x@email.com", DepartmentId = 1 };
            await Assert.ThrowsAsync<NotFoundException>(() => SellerService.UpdateAsync(seller));
        }
    }
}

