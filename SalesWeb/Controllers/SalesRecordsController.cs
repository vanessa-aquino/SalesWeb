using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Text;
using SalesWeb.Services;
using SalesWeb.Extensions;

namespace SalesWeb.Controllers
{
    [Authorize]
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate, string sellerName)
        {
            if (!minDate.HasValue) minDate = new DateTime(DateTime.Now.Year, 1, 1);
            if (!maxDate.HasValue) maxDate = DateTime.Now;

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            ViewData["sellerName"] = sellerName;

            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate, sellerName);
            return View(result);
        }
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate, string sellerName, int? departmentId)
        {
            if (!minDate.HasValue) minDate = new DateTime(DateTime.Now.Year, 1, 1);
            if (!maxDate.HasValue) maxDate = DateTime.Now;

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            ViewData["sellerName"] = sellerName;
            ViewData["departmentId"] = departmentId;

            var departments = await _salesRecordService.GetDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", departmentId);

            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate, sellerName, departmentId);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ExportCsvAsync(DateTime? minDate, DateTime? maxDate, string sellerName)
        {
            if (!minDate.HasValue) minDate = new DateTime(DateTime.Now.Year, 1, 1);
            if (!maxDate.HasValue) maxDate = DateTime.Now;

            var records = await _salesRecordService.FindByDateAsync(minDate, maxDate, sellerName);
            var uS = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            var lines = new List<string>
            {
                $"Id{uS}Data{uS}Vendedor{uS}Departamento{uS}Valor{uS}Status"
            };

            foreach (var r in records)
            {
                var line = $"{r.Id}{uS}{r.Date:dd/MM/yyyy}{uS}{r.Seller.Name}{uS}{r.Seller.Department.Name}{uS}R${r.Amount:F2}{uS}{r.Status.GetDisplayName()}";
                lines.Add(line);
            }

            var csv = string.Join(Environment.NewLine, lines);
            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv)).ToArray();
            var fileName = $"Relatorio_Vendas_{DateTime.Now:yyyyMMdd}.csv";

            return File(bytes, "text/csv", fileName);

        }
    }
}

