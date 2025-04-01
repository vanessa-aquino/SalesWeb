using Microsoft.AspNetCore.Mvc;
using SalesWeb.Services;

namespace SalesWeb.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

    public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public async Task<IActionResult> Index()
        {
            var sellers = await _sellerService.FindAll();
            return View(sellers);
        }
    }
}
