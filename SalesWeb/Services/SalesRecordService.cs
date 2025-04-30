using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;

namespace SalesWeb.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebContext _context;

        public SalesRecordService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _context.Department.OrderBy(dep => dep.Name).ToListAsync();
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate, string sellerName)
        {
            // Pega o meu SalesRecords, que é do tipo DbSet (corresponde a uma tabela do banco de dados e permite que vc execute consultas e operações de manipulação de dados)
            // E constroi um objeto (result) do tipo IQueryable, me permitindo consultas de forma dinâmica ao banco de dados.
            var result = from obj in _context.SalesRecords select obj;

            if(minDate.HasValue) result = result.Where(x => x.Date >= minDate.Value);
            if(maxDate.HasValue) result = result.Where(x => x.Date <= maxDate.Value);
            if(!string.IsNullOrEmpty(sellerName)) result = result.Where(x => x.Seller.Name.ToLower().Contains(sellerName.ToLower()));

            return await result
                .Include(x  => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();       
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate, string sellerName, int? departmentId)
        {
            var result = from obj in _context.SalesRecords select obj;

            if (minDate.HasValue) result = result.Where(x => x.Date >= minDate.Value);
            if (maxDate.HasValue) result = result.Where(x => x.Date <= maxDate.Value);
            if(!string.IsNullOrEmpty(sellerName)) result = result.Where(x => x.Seller.Name.ToLower().ToLower().Contains(sellerName.ToLower()));
            if(departmentId.HasValue) result = result.Where(x => x.Seller.Department.Id == departmentId.Value);

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
