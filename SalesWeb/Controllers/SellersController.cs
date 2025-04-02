using Microsoft.AspNetCore.Mvc;
using SalesWeb.Models;
using SalesWeb.Services;
using SalesWeb.ViewModels;
using SalesWeb.Services.Exceptions;

namespace SalesWeb.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var sellers = await _sellerService.FindAll();
            return View(sellers);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };    
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {

            await _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) NotFound();

            var seller = await _sellerService.FindById(id.Value);
            if(seller == null) NotFound();

            return View(seller);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) NotFound();

            var seller = await _sellerService.FindById(id.Value);
            if(seller == null) NotFound();

            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) NotFound();

            var seller = await _sellerService.FindById(id.Value);
            if(seller == null) NotFound();

            List<Department> departments = await _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id != seller.Id) BadRequest();

            try
            {
            await _sellerService.Update(seller);
            return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
