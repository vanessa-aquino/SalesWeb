using SalesWeb.Models;
using SalesWeb.Tests.Common;

namespace SalesWeb.Tests.Services
{
    public class DepartmentServiceTests : BaseTest
    {
        [Fact]
        public async Task FindAllAsync_ReturnDepartmentList()
        {
            var result = await DepartmentService.FindAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<Department>>(result);
            Assert.Equal(1, result.Count);
        }
    }
}
