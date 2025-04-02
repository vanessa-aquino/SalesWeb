using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;
using SalesWeb.Services.Exceptions;

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

        public async Task<Seller> FindById(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task Remove(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Seller seller)
        {
            if (!_context.Seller.Any(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException err)
            {
                throw new DbUpdateConcurrencyException(err.Message);
            }
        }
    }
}
