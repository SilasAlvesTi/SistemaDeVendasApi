using Microsoft.EntityFrameworkCore;
using SistemaDeVendasApi.Data;
using SistemaDeVendasApi.Models.VendaModels;

namespace SistemaDeVendasApi.Repositories
{
    public class ItemVendaRepository : IRepository<ItemVenda>
    {
        private readonly SistemaDeVendasContext _context;

        public ItemVendaRepository(SistemaDeVendasContext context)
        {
            _context = context;
        }
        public async Task<ItemVenda> AdicionarAsync(ItemVenda entidade)
        {
            entidade.ItemVendaID = 0;
            _context.ItensVenda.Add(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

        public Task ApagarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemVenda> EditarAsync(int id, ItemVenda entidade)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemVenda>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemVenda?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
