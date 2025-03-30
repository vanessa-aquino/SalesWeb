using Microsoft.EntityFrameworkCore;
using SalesWeb.Models;


namespace SalesWeb.Data
{
    public class SalesWebContext : DbContext // -DbContext é a classe central do Entity para trabalhar com banco de dados
    {
        // O construtor recebe as opções de configuração e as passa para a classe dbCOntext através do : base(options)
        public SalesWebContext (DbContextOptions<SalesWebContext> options)
            : base(options)
        {
        }

        // O DBSet é uma coleção de objetos que representam uma tabela de banco de dados
        public DbSet<Department> Department { get; set; } = default!; // O default é para evitar um aviso de que a propriedade Department pode ser null
        public DbSet<Seller> Seller { get; set; } = default!;
        public DbSet<SalesRecord> SalesRecords { get; set; } = default!;
    }
}
