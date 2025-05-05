using SalesWeb.Controllers;
using SalesWeb.Data;
using SalesWeb.Models.Enums;
using SalesWeb.Services;
using SalesWeb.Tests.Utils;

namespace SalesWeb.Tests.Common
{
    public abstract class BaseTest
    {
        protected SalesWebContext Context { get; private set; }
        protected SellerService SellerService { get; private set; }
        protected DepartmentService DepartmentService { get; private set; }
        protected SalesRecordService SalesRecordService { get; private set; }
        protected SalesStatus SalesStatus { get; private set; }
        protected DepartmentsController DepartmentsController { get; private set; }
        protected SalesRecordsController SalesRecordsController { get; private set; }
        protected SellersController SellersController { get; private set; }
        protected BaseTest()
        {
            Context = TestDbContextFactory.GetInMemoryDbContext();
            SellerService = new SellerService(Context);  // Cria o SellerService com o novo banco de dados criado
            DepartmentService = new DepartmentService(Context);
            SalesRecordService = new SalesRecordService(Context);
            DepartmentsController = new DepartmentsController(Context);
            SalesRecordsController = new SalesRecordsController(SalesRecordService);
            SellersController = new SellersController(SellerService, DepartmentService);
        }
    }
}