using Microsoft.EntityFrameworkCore;
using SistemaDeVendasApi.Models.Cliente;

namespace SistemaDeVendasApi.Data
{
    public class SistemaDeVendasContext : DbContext
    {
        public SistemaDeVendasContext(DbContextOptions<SistemaDeVendasContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
