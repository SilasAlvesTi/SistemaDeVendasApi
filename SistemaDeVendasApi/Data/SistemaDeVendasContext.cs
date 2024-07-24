using Microsoft.EntityFrameworkCore;
using SistemaDeVendasApi.Models.ClienteModels;
using SistemaDeVendasApi.Models.ProdutoModels;
using SistemaDeVendasApi.Models.VendaModels;

namespace SistemaDeVendasApi.Data
{
    public class SistemaDeVendasContext : DbContext
    {
        public SistemaDeVendasContext(DbContextOptions<SistemaDeVendasContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
    }
}
