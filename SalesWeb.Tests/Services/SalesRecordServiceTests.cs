using SalesWeb.Models;
using SalesWeb.Services;
using SalesWeb.Tests.Common;

namespace SalesWeb.Tests.Services
{
    public class SalesRecordServiceTests : BaseTest
    {
        [Fact]
        public async Task FindByDateAsync_ReturnSalesByDate()
        {
            var minDate = DateTime.Now.AddDays(-15);
            var maxDate = DateTime.Now;
            var sellerName = "Primeiro";
            var salesRecord = new SalesRecordService(Context);

            var result = await salesRecord.FindByDateAsync(minDate, maxDate, sellerName);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.All(result, r =>
            {
                Assert.True(r.Date >= minDate && r.Date <= maxDate);
                Assert.Contains(sellerName.ToLower(), r.Seller.Name.ToLower());
            });
        }

        [Fact]
        public async Task FindByDateAsync_ReturnSallesBySellerName()
        {
            var sellerName = "Primeiro";
            var salesRecord = new SalesRecordService(Context);


            var result = await salesRecord.FindByDateAsync(null, null, sellerName);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All(result, r =>
            {
                Assert.Contains(sellerName.ToLower(), r.Seller.Name.ToLower());
            });
        }

        [Fact]
        public async Task FindByDateGroupingAsync_ReturnSalesByDepartments()
        {
            var minDate = DateTime.Now.AddDays(-15);
            var maxDate = DateTime.Now;
            var sellerName = "Primeiro";
            var departmentId = 1;
            var salesRecord = new SalesRecordService(Context);


            var result = await salesRecord.FindByDateGroupingAsync(minDate, maxDate, sellerName, departmentId);

            Assert.NotNull(result);
            var group = result.First();
            Assert.Equal(departmentId, group.Key.Id);
            Assert.All(group, r =>
            {
                Assert.Contains(sellerName.ToLower(), r.Seller.Name.ToLower());
                Assert.Equal(departmentId, r.Seller.Department.Id);
                Assert.True(r.Date >= minDate && r.Date <= maxDate);
            });
        }

    }
}
