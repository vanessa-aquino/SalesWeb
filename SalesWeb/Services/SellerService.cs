using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;

namespace SalesWeb.Services
{
    public class SellerService
    {
        private readonly SalesWebContext _context;

        public SellerService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAll()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task Insert(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

        }
    }
}
