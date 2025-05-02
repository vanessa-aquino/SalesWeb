using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using SalesWeb.Models;
using SalesWeb.Tests.Common;

namespace SalesWeb.Tests.Controllers
{
    public class DepartmentsControllerTests : BaseTest
    {
        // Index
        [Fact]
        public async Task Index_ReturnDepartmentsList()
        {
            var result = await DepartmentsController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Department>>(viewResult.Model);
            Assert.Single(model);
        }

        //Details
        [Fact]
        public async Task Details_ValidId_ReturnsDepartment()
        {
            var id = 1;
            var result = await DepartmentsController.Details(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var department = Assert.IsType<Department>(viewResult.Model);
            Assert.NotNull(result);
            Assert.Equal("TI", department.Name);
        }

        [Fact]
        public async Task Details_DepartmentNotFound_ReturnNotFound()
        {
            var id = 999;
            var result = await DepartmentsController.Details(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_InvalidId_ReturnNotFound()
        {
            var result = await DepartmentsController.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        //Create
        [Fact]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            var newDept = new Department { Id = 2, Name = "Financeiro" };
            var result = await DepartmentsController.Create(newDept);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Equal(2, Context.Department.Count());
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsViewWithModel()
        {
            DepartmentsController.ModelState.AddModelError("Name", "Required field");

            var department = new Department { Id = 11 };
            var result = await DepartmentsController.Create(department);

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Department>(view.Model);
            Assert.Equal(11, model.Id);
        }

        //Edit
        [Fact]
        public async Task EditGet_InvalidId_ReturnsNotFound()
        {
            var result = await DepartmentsController.Edit(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditGet_ValidId_ReturnsViewWithModel()
        {
            var id = 1;
            var result = await DepartmentsController.Edit(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Department>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task EditGet_DepartmentNotFound_ReturnsNotFound()
        {
            var id = 999;
            var result = await DepartmentsController.Edit(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditPost_IdMismatch_ReturnsNotFound()
        {
            var dept = new Department { Id = 2, Name = "Financeiro" };
            var result = await DepartmentsController.Edit(1, dept);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditPost_InvalidModelState_ReturnsViewWithModel()
        {
            DepartmentsController.ModelState.AddModelError("Name", "Required field");
            var department = new Department { Id = 11 };

            var result = await DepartmentsController.Edit(11, department);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(department, viewResult.Model);

        }

        [Fact]
        public async Task EditPost_ValidUpdate_RedirecsToIndex()
        {
            var existing = await Context.Department.FindAsync(1);
            Context.Entry(existing).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            var department = new Department { Id = 1, Name = "Atualizado" };
            var result = await DepartmentsController.Edit(1, department);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);

            var update = await Context.Department.FindAsync(1);
            Assert.Equal("Atualizado", update.Name);
        }

        //Delete
        [Fact]
        public async Task DeleteGet_InvalidId_ReturnsNotFound()
        {
            var result = await DepartmentsController.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteGet_ValidId_ReturnsViewWithModel()
        {
            var id = 1;
            var result = await DepartmentsController.Delete(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Department>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task DeleteGet_DepartmentNotFound_ReturnsNotFound()
        {
            var id = 999;
            var result = await DepartmentsController.Delete(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_DepartmentsExists_DeletesAndRedirects()
        {
            var dept = new Department { Id = 99, Name = "Deletar" };
            Context.Department.Add(dept);
            await Context.SaveChangesAsync();

            var result = await DepartmentsController.DeleteConfirmed(99);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.DoesNotContain(Context.Department, d => d.Id == 99);
        }

        [Fact]
        public async Task DeleteConfirmed_DepartmentNotFound_StillRedirects()
        {
            var id = 200;
            var result = await DepartmentsController.DeleteConfirmed(id);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
    }
}
