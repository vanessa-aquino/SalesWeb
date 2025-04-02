using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;

namespace SalesWeb.Services
{
    public class DepartmentService
    {
        private readonly SalesWebContext _context;

        public DepartmentService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAll()
        {
            return await _context.Department.OrderBy(dep => dep.Name).ToListAsync();
        }
    }
}
