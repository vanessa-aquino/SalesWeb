using Microsoft.EntityFrameworkCore;
using SalesWeb.Models;
using SalesWeb.Models.Enums;

namespace SalesWeb.Data
{
    public class SeedingService
    {
        private readonly SalesWebContext _context;

        public SeedingService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (await _context.Department.AnyAsync() ||
                await _context.Seller.AnyAsync() ||
                await _context.SalesRecords.AnyAsync())
            {
                return; // Banco de dados já foi populado
            }
            var departments = new List<Department>
            {
            new Department(1, "Electronics"),
            new Department(2, "Home appliances"),
            new Department(3, "Furniture & Decoration"),
            new Department(4, "Sports & Leisure"),
            };

            await _context.Department.AddRangeAsync(departments);
            await _context.SaveChangesAsync();

            var sellers = new List<Seller>
            {
            new Seller(1, "Ana Beatriz Oliveira", "ana.oliveira@email.com", new DateTime(1992, 3, 15), 2300.0, departments[0]),
            new Seller(2, "Ana Clara Lima", "lima.clara@email.com", new DateTime(1995, 7, 22), 2200.0, departments[1]),
            new Seller(3, "Camila Souza Mendes", "camila.mendes@email.com", new DateTime(1995, 10, 11), 2100.0, departments[2]),
            new Seller(4, "Diego Rocha Martins", "diego.martins@email.com", new DateTime(1990, 5, 5), 2000.0, departments[3]),
            new Seller(5, "Eduardo Santos Reis", "eduardo.reis@email.com", new DateTime(1985, 9, 18), 2300.0, departments[0]),
            new Seller(6, "Fernanda Alves Costa", "fernanda.costa@email.com", new DateTime(1993, 12, 30), 2200.0, departments[1]),
            new Seller(7, "Gustavo Nunes Pereira", "gustavo.pereira@email.com", new DateTime(1989, 6, 25), 2100.0, departments[2]),
            new Seller(8, "Helena Lima Rocha", "helena.rocha@email.com", new DateTime(1994, 2, 7), 2000.0, departments[3]),
            };

            await _context.Seller.AddRangeAsync(sellers);
            await _context.SaveChangesAsync();

            var salesRecord = new List<SalesRecord>
            {
            new SalesRecord(1, new DateTime(2025, 1, 2), 850, SalesStatus.Billed, sellers[0]),
            new SalesRecord(2, new DateTime(2025, 1, 2), 2200, SalesStatus.Billed, sellers[0]),
            new SalesRecord(3, new DateTime(2025, 1, 2), 750, SalesStatus.Pending, sellers[3]),
            new SalesRecord(4, new DateTime(2025, 1, 2), 1300, SalesStatus.Billed, sellers[3]),
            new SalesRecord(5, new DateTime(2025, 1, 3), 1200, SalesStatus.Canceled, sellers[4]),
            new SalesRecord(6, new DateTime(2025, 1, 3), 3500, SalesStatus.Billed, sellers[4]),
            new SalesRecord(7, new DateTime(2025, 1, 3), 2300, SalesStatus.Pending, sellers[6]),
            new SalesRecord(8, new DateTime(2025, 1, 3), 850, SalesStatus.Billed, sellers[7]),
            new SalesRecord(9, new DateTime(2025, 1, 5), 950, SalesStatus.Billed, sellers[0]),
             new SalesRecord(10, new DateTime(2025, 1, 5), 2000, SalesStatus.Canceled, sellers[1]),
            new SalesRecord(11, new DateTime(2025, 1, 5), 600, SalesStatus.Pending, sellers[1]),
            new SalesRecord(12, new DateTime(2025, 1, 5), 3500, SalesStatus.Billed, sellers[2]),
            new SalesRecord(13, new DateTime(2025, 1, 7), 1700, SalesStatus.Pending, sellers[4]),
            new SalesRecord(14, new DateTime(2025, 1, 7), 1200, SalesStatus.Billed, sellers[7]),
            new SalesRecord(15, new DateTime(2025, 1, 7), 1800, SalesStatus.Canceled, sellers[6]),
            new SalesRecord(16, new DateTime(2025, 1, 7), 2500, SalesStatus.Billed, sellers[6]),
            new SalesRecord(17, new DateTime(2025, 1, 9), 950, SalesStatus.Canceled, sellers[0]),
            new SalesRecord(18, new DateTime(2025, 1, 9), 3500, SalesStatus.Billed, sellers[1]),
            new SalesRecord(19, new DateTime(2025, 1, 9), 1300, SalesStatus.Pending, sellers[2]),
            new SalesRecord(20, new DateTime(2025, 1, 9), 2000, SalesStatus.Billed, sellers[2]),
            new SalesRecord(21, new DateTime(2025, 1, 11), 1100, SalesStatus.Billed, sellers[4]),
            new SalesRecord(22, new DateTime(2025, 1, 11), 2100, SalesStatus.Pending, sellers[5]),
            new SalesRecord(23, new DateTime(2025, 1, 11), 1800, SalesStatus.Billed, sellers[7]),
            new SalesRecord(24, new DateTime(2025, 1, 11), 2200, SalesStatus.Canceled, sellers[7]),
            new SalesRecord(25, new DateTime(2025, 1, 12), 800, SalesStatus.Billed, sellers[1]),
            new SalesRecord(26, new DateTime(2025, 1, 12), 1500, SalesStatus.Canceled, sellers[1]),
            new SalesRecord(27, new DateTime(2025, 1, 12), 2200, SalesStatus.Billed, sellers[2]),
            new SalesRecord(28, new DateTime(2025, 1, 12), 2500, SalesStatus.Pending, sellers[0]),
            new SalesRecord(29, new DateTime(2025, 1, 13), 1200, SalesStatus.Canceled, sellers[4]),
            new SalesRecord(30, new DateTime(2025, 1, 13), 2200, SalesStatus.Billed, sellers[7]),
            new SalesRecord(31, new DateTime(2025, 1, 13), 1600, SalesStatus.Pending, sellers[5]),
            new SalesRecord(32, new DateTime(2025, 1, 13), 2500, SalesStatus.Billed, sellers[7]),
            new SalesRecord(33, new DateTime(2025, 1, 14), 900, SalesStatus.Billed, sellers[3]),
            new SalesRecord(34, new DateTime(2025, 1, 14), 2100, SalesStatus.Canceled, sellers[3]),
            new SalesRecord(35, new DateTime(2025, 1, 14), 1200, SalesStatus.Pending, sellers[2]),
            new SalesRecord(36, new DateTime(2025, 1, 14), 2800, SalesStatus.Billed, sellers[3]),
            new SalesRecord(37, new DateTime(2025, 1, 16), 1500, SalesStatus.Billed, sellers[4]),
            new SalesRecord(38, new DateTime(2025, 1, 16), 2500, SalesStatus.Pending, sellers[7]),
            new SalesRecord(39, new DateTime(2025, 1, 16), 1300, SalesStatus.Billed, sellers[6]),
            new SalesRecord(40, new DateTime(2025, 1, 16), 3200, SalesStatus.Canceled, sellers[7]),
            new SalesRecord(41, new DateTime(2025, 1, 18), 1000, SalesStatus.Billed, sellers[2]),
            new SalesRecord(42, new DateTime(2025, 1, 18), 2100, SalesStatus.Canceled, sellers[0]),
            new SalesRecord(43, new DateTime(2025, 1, 18), 1400, SalesStatus.Billed, sellers[2]),
            new SalesRecord(44, new DateTime(2025, 1, 18), 1800, SalesStatus.Pending, sellers[4]),
            new SalesRecord(45, new DateTime(2025, 1, 19), 2700, SalesStatus.Billed, sellers[4]),
            new SalesRecord(46, new DateTime(2025, 1, 19), 1900, SalesStatus.Canceled, sellers[1]),
            new SalesRecord(47, new DateTime(2025, 1, 19), 2400, SalesStatus.Pending, sellers[1]),
            new SalesRecord(48, new DateTime(2025, 1, 19), 3000, SalesStatus.Billed, sellers[7]),
            new SalesRecord(49, new DateTime(2025, 1, 20), 1350, SalesStatus.Billed, sellers[0]),
            new SalesRecord(50, new DateTime(2025, 1, 20), 2700, SalesStatus.Canceled, sellers[1]),
            new SalesRecord(51, new DateTime(2025, 1, 20), 2100, SalesStatus.Billed, sellers[2]),
            new SalesRecord(52, new DateTime(2025, 1, 20), 1300, SalesStatus.Pending, sellers[3]),
            new SalesRecord(53, new DateTime(2025, 2, 2), 2200, SalesStatus.Billed, sellers[0]),
            new SalesRecord(54, new DateTime(2025, 2, 2), 3200, SalesStatus.Canceled, sellers[1]),
            new SalesRecord(55, new DateTime(2025, 2, 2), 1500, SalesStatus.Pending, sellers[2]),
            new SalesRecord(56, new DateTime(2025, 2, 2), 2000, SalesStatus.Billed, sellers[2]),
            new SalesRecord(57, new DateTime(2025, 2, 3), 2400, SalesStatus.Pending, sellers[7]),
            new SalesRecord(58, new DateTime(2025, 2, 3), 1800, SalesStatus.Billed, sellers[5]),
            new SalesRecord(59, new DateTime(2025, 2, 3), 3000, SalesStatus.Canceled, sellers[6]),
            new SalesRecord(60, new DateTime(2025, 2, 3), 2200, SalesStatus.Billed, sellers[7]),
            new SalesRecord(61, new DateTime(2025, 2, 4), 1100, SalesStatus.Pending, sellers[3]),
            new SalesRecord(62, new DateTime(2025, 2, 4), 2500, SalesStatus.Billed, sellers[1]),
            new SalesRecord(63, new DateTime(2025, 2, 4), 1800, SalesStatus.Canceled, sellers[0]),
            new SalesRecord(64, new DateTime(2025, 2, 4), 3200, SalesStatus.Billed, sellers[3]),
            new SalesRecord(65, new DateTime(2025, 2, 5), 1400, SalesStatus.Billed, sellers[4]),
            new SalesRecord(66, new DateTime(2025, 2, 5), 2500, SalesStatus.Pending, sellers[4]),
            new SalesRecord(67, new DateTime(2025, 2, 5), 1900, SalesStatus.Billed, sellers[3]),
            new SalesRecord(68, new DateTime(2025, 2, 5), 2800, SalesStatus.Canceled, sellers[7]),
            new SalesRecord(69, new DateTime(2025, 2, 6), 2200, SalesStatus.Billed, sellers[0]),
            new SalesRecord(70, new DateTime(2025, 2, 6), 1500, SalesStatus.Pending, sellers[1]),
            new SalesRecord(71, new DateTime(2025, 2, 6), 3200, SalesStatus.Billed, sellers[1]),
            new SalesRecord(72, new DateTime(2025, 2, 6), 2500, SalesStatus.Canceled, sellers[3]),
            new SalesRecord(73, new DateTime(2025, 2, 7), 2100, SalesStatus.Pending, sellers[4]),
            new SalesRecord(74, new DateTime(2025, 2, 7), 2700, SalesStatus.Billed, sellers[6]),
            new SalesRecord(75, new DateTime(2025, 2, 7), 2300, SalesStatus.Billed, sellers[6]),
            new SalesRecord(76, new DateTime(2025, 2, 7), 1600, SalesStatus.Canceled, sellers[7]),
            new SalesRecord(77, new DateTime(2025, 2, 8), 1800, SalesStatus.Pending, sellers[7]),
            new SalesRecord(78, new DateTime(2025, 2, 8), 2000, SalesStatus.Billed, sellers[2]),
            new SalesRecord(79, new DateTime(2025, 2, 8), 2200, SalesStatus.Billed, sellers[2]),
            new SalesRecord(80, new DateTime(2025, 2, 8), 2400, SalesStatus.Canceled, sellers[2]),
            new SalesRecord(81, new DateTime(2025, 2, 9), 2800, SalesStatus.Billed, sellers[3]),
            new SalesRecord(82, new DateTime(2025, 2, 9), 2100, SalesStatus.Pending, sellers[5]),
            new SalesRecord(83, new DateTime(2025, 2, 9), 1800, SalesStatus.Billed, sellers[5]),
            new SalesRecord(84, new DateTime(2025, 2, 9), 2500, SalesStatus.Canceled, sellers[7]),
            new SalesRecord(85, new DateTime(2025, 2, 10), 1300, SalesStatus.Pending, sellers[2]),
            new SalesRecord(86, new DateTime(2025, 2, 10), 3000, SalesStatus.Billed, sellers[2]),
            new SalesRecord(87, new DateTime(2025, 2, 10), 2200, SalesStatus.Billed, sellers[4]),
            new SalesRecord(88, new DateTime(2025, 2, 10), 2700, SalesStatus.Canceled, sellers[4]),
            new SalesRecord(89, new DateTime(2025, 2, 12), 2500, SalesStatus.Billed, sellers[4]),
            new SalesRecord(90, new DateTime(2025, 2, 12), 2200, SalesStatus.Pending, sellers[5]),
            new SalesRecord(91, new DateTime(2025, 2, 12), 3200, SalesStatus.Billed, sellers[6]),
            new SalesRecord(92, new DateTime(2025, 2, 12), 2900, SalesStatus.Canceled, sellers[7]),
            new SalesRecord(93, new DateTime(2025, 2, 13), 2500, SalesStatus.Billed, sellers[2]),
            new SalesRecord(94, new DateTime(2025, 2, 13), 2300, SalesStatus.Pending, sellers[2]),
            new SalesRecord(95, new DateTime(2025, 2, 13), 1900, SalesStatus.Billed, sellers[2]),
            new SalesRecord(96, new DateTime(2025, 2, 13), 2200, SalesStatus.Canceled, sellers[0]),
            new SalesRecord(97, new DateTime(2025, 2, 14), 1800, SalesStatus.Billed, sellers[4]),
            new SalesRecord(98, new DateTime(2025, 2, 14), 2700, SalesStatus.Canceled, sellers[4]),
            new SalesRecord(99, new DateTime(2025, 2, 14), 2100, SalesStatus.Pending, sellers[6]),
            new SalesRecord(100, new DateTime(2025, 2, 15), 2200, SalesStatus.Billed, sellers[5]),
            new SalesRecord(101, new DateTime(2025, 3, 1), 2500, SalesStatus.Billed, sellers[0]),
            new SalesRecord(102, new DateTime(2025, 3, 1), 2200, SalesStatus.Pending, sellers[1]),
            new SalesRecord(103, new DateTime(2025, 3, 1), 1700, SalesStatus.Billed, sellers[1]),
            new SalesRecord(104, new DateTime(2025, 3, 1), 2600, SalesStatus.Canceled, sellers[1]),
            new SalesRecord(105, new DateTime(2025, 3, 2), 2300, SalesStatus.Billed, sellers[4]),
            new SalesRecord(106, new DateTime(2025, 3, 2), 1900, SalesStatus.Pending, sellers[5]),
            new SalesRecord(107, new DateTime(2025, 3, 2), 1800, SalesStatus.Canceled, sellers[7]),
            new SalesRecord(108, new DateTime(2025, 3, 2), 2400, SalesStatus.Billed, sellers[7]),
            new SalesRecord(109, new DateTime(2025, 3, 3), 2700, SalesStatus.Billed, sellers[0]),
            new SalesRecord(110, new DateTime(2025, 3, 3), 2100, SalesStatus.Canceled, sellers[1]),
            new SalesRecord(111, new DateTime(2025, 3, 3), 2300, SalesStatus.Billed, sellers[1]),
            new SalesRecord(112, new DateTime(2025, 3, 3), 2000, SalesStatus.Pending, sellers[3]),
            new SalesRecord(113, new DateTime(2025, 3, 4), 2500, SalesStatus.Billed, sellers[4]),
            new SalesRecord(114, new DateTime(2025, 3, 4), 2200, SalesStatus.Pending, sellers[7]),
            new SalesRecord(115, new DateTime(2025, 3, 4), 2700, SalesStatus.Canceled, sellers[6]),
            new SalesRecord(116, new DateTime(2025, 3, 4), 1900, SalesStatus.Billed, sellers[7]),
            };

            await _context.SalesRecords.AddRangeAsync(salesRecord);
            await _context.SaveChangesAsync();

        }
    }
}
