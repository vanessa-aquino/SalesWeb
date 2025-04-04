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

        public async Task<List<SalesRecord>> FindByDate(DateTime? minDate, DateTime? maxDate)
        {
            // Pega o meu SalesRecords, que é do tipo DbSet (corresponde a uma tableda do banco de dados e permite que vc execute consultas e operações de manipulação de dados)
            // E constroi um objeto (result) do tipo IQueryable, me permitindo consultas de forma dinâmica ao banco de dados.
            var result = from obj in _context.SalesRecords select obj;

            if(minDate.HasValue) result = result.Where(x => x.Date >= minDate.Value);
            if(maxDate.HasValue) result = result.Where(x => x.Date <= maxDate.Value);

            return await result
                .Include(x  => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();       
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGrouping(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecords select obj;

            if (minDate.HasValue) result = result.Where(x => x.Date >= minDate.Value);
            if (maxDate.HasValue) result = result.Where(x => x.Date <= maxDate.Value);

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
