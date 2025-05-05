using Microsoft.AspNetCore.Mvc;
using SalesWeb.Models;
using SalesWeb.Tests.Common;
using SalesWeb.ViewModels;

namespace SalesWeb.Tests.Controllers
{
    public class SellersControllerTests : BaseTest
    {
        [Fact]
        public async Task Index_ReturnsViewWithSellers()
        {
            var result = await SellersController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Seller>>(viewResult.Model);
        }

        [Fact]
        public async Task CreateGet_ReturnsViewWithDepartments()
        {
            var result = await SellersController.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SellerFormViewModel>(viewResult.Model);
            Assert.Equal(Context.Department, model.Departments);
        }

        [Fact]
        public async Task CreatePost_ValidModel_RedirectsToIndex()
        {
            var seller = new Seller
            {
                Id = 10,
                Name = "Fulano",
                Email = "email@email.com",
                BirthDate = new DateTime(2002, 3, 5),
                BaseSalary = 1000.00,
                DepartmentId = 1,
            };

            var result = await SellersController.Create(seller);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SellerFormViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task DeleteGet_InvalidId_RedirectToError()
        {
            var result = await SellersController.Delete(null);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Id not provided", redirect.RouteValues["message"]);
        }

        [Fact]
        public async Task DeletGet_ValidID_ReturnsSeller()
        {
            var id = 1;
            var result = await SellersController.Delete(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Seller>(viewResult.Model);
            Assert.Equal(id, model.Id);
        }

        [Fact]
        public async Task DeleteGet_SellerNotFound_RedirectToError()
        {
            var id = 999;
            var result = await SellersController.Delete(id);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Id not found", redirect.RouteValues["message"]);
        }

        [Fact]
        public async Task DeleteConfirmed_RemoveSeller()
        {
            var id = 1;
            var result = await SellersController.DeleteConfirmed(id);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Details_InvalidIdRedirecterror()
        {
            var result = await SellersController.Details(null);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Id not provided", redirect.RouteValues["message"]);
        }

        [Fact]
        public async Task Details_ValidId_ReturnsSellers()
        {
            var id = 1;
            var result = await SellersController.Details(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var seller = Assert.IsType<Seller>(viewResult.Model);
            Assert.NotNull(result);
            Assert.Equal("Primeiro Nome", seller.Name);
        }

        [Fact]
        public async Task Details_SellerNotFound_RedirectToError()
        {
            var id = 999;
            var result = await SellersController.Details(id);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Id not found", redirect.RouteValues["message"]);
        }

        [Fact]
        public async Task EditGet_InvalidIdRedirecterror()
        {
            var result = await SellersController.Edit(null);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Id not provided", redirect.RouteValues["message"]);
        }

        [Fact]
        public async Task EditGet_SellerNotFound_RedirectToError()
        {
            var id = 999;
            var result = await SellersController.Edit(id);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Id not found", redirect.RouteValues["message"]);
        }

        [Fact]
        public async Task EditGet_ValidId_ReturnsViewWithModel()
        {
            var id = 1;
            var result = await SellersController.Edit(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SellerFormViewModel>(viewResult.Model);
            Assert.NotNull(model.Seller);
            Assert.NotNull(model.Departments);
            Assert.Equal(id, model.Seller.Id);
        }

        [Fact]
        public async Task EditPost_InvalidModelState_ReturnsViewWithViewModel()
        {

            var seller = new Seller
            {
                Id = 50,
                Name = "Teste",
                Email = "teste@email.com",
                BirthDate = new DateTime(1990, 1, 1),
                BaseSalary = 1000.00,
                DepartmentId = 1
            };

            var result = await SellersController.Edit(50, seller);

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SellerFormViewModel>(view.Model);
            Assert.Equal(seller, model.Seller);
            Assert.NotNull(model.Departments);
        }
    }
}
