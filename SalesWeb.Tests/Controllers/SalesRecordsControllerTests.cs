using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWeb.Models;
using SalesWeb.Tests.Common;

using System.Text;

namespace SalesWeb.Tests.Controllers
{
    public class SalesRecordsControllerTests : BaseTest
    {
        [Fact]
        public async Task SimpleSearch_NullDates_ReturnsCurrentYearAndDateTimeNow()
        {
            var result = await SalesRecordsController.SimpleSearch(null, null, "Primeiro");
            var minDateString = SalesRecordsController.ViewData["minDate"]?.ToString();
            var parsedMinDate = DateTime.Parse(minDateString);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
            Assert.Equal(DateTime.Now.Year, parsedMinDate.Year);
            Assert.NotNull(SalesRecordsController.ViewData["maxDate"]);
            Assert.Equal("Primeiro", SalesRecordsController.ViewData["sellerName"]);
        }

        [Fact]
        public async Task GroupingSearch_NullParameters_ReturnsCurrentYearAndDateTimeNow()
        {
            var result = await SalesRecordsController.GroupingSearch(null, null, null, null);

            var minDateString = SalesRecordsController.ViewData["minDate"]?.ToString();
            var parsedMinDate = DateTime.Parse(minDateString);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
            Assert.Equal(DateTime.Now.Year, parsedMinDate.Year);
            Assert.NotNull(SalesRecordsController.ViewData["maxDate"]);
        }

        [Fact]
        public async Task GroupingSearch_SellerNameAndDepartmentId_AssignedToViewData()
        {
            var departmentId = 1;
            var result = await SalesRecordsController.GroupingSearch(null, null, "Primeiro", departmentId);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Primeiro", viewResult.ViewData["sellerName"]);
            Assert.Equal(departmentId, viewResult.ViewData["departmentId"]);
        }

        [Fact]
        public async Task GroupingSearch_AssignsDepartmentsToViewBag()
        {
            var result = await SalesRecordsController.GroupingSearch(null, null, null, null);
            
            var viewResult = Assert.IsType<ViewResult>(result);
            var  departments = viewResult.ViewData["Departments"] ?? SalesRecordsController.ViewBag.Departments;
            
            Assert.NotNull(departments);
            Assert.IsType<SelectList>(departments);
        }

        [Fact]
        public async Task GroupingSearch_ReturnsViewWithGroupedSales()
        {
            var result = await SalesRecordsController.GroupingSearch(null, null, null, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<IGrouping<object, SalesRecord>>>(viewResult.Model);
            Assert.NotNull(model);   
        }

        [Fact]
        public async Task ExpotyCsvAsync_ReturnsFileResult()
        {
            var result = await SalesRecordsController.ExportCsvAsync(null, null, null);

            var FileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("text/csv", FileResult.ContentType);
            Assert.Contains("Relatorio_Vendas_", FileResult.FileDownloadName);

        }

        [Fact]
        public async Task ExportCsvAsync_CsvStartsWithHeader()
        {
            var result = await SalesRecordsController.ExportCsvAsync(null, null, null) as FileContentResult;
            var csvContent = Encoding.UTF8.GetString(result.FileContents);

            Assert.StartsWith("Id", csvContent);
            Assert.Contains("Vendedor", csvContent);
        }

        [Fact]
        public async Task ExportCsvAsync_CsvContainsSalesData()
        {
            var result = await SalesRecordsController.ExportCsvAsync(null, null, "Primeiro") as FileContentResult;
            var csvContent = Encoding.UTF8.GetString(result.FileContents);
            
            Assert.Contains("Primeiro", csvContent);
        }
    }
}
