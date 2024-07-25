using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SistemaDeVendasApi.Data;
using SistemaDeVendasApi.Models.VendaModels;

namespace SistemaDeVendasApi.Repositories
{
    public class VendaRepository : IRepository<Venda>
    {
        private readonly SistemaDeVendasContext _context;

        public VendaRepository(SistemaDeVendasContext context)
        {
            _context = context;
        }

        public async Task<Venda> AdicionarAsync(Venda venda)
        {
            foreach (var item in venda.ItensVenda)
            {
                item.VendaID = venda.VendaID;
                _context.ItensVenda.Add(item);
            }
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            return venda;
        }

        public async Task ApagarAsync(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Venda> EditarAsync(int id, Venda venda)
        {

            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();

            return venda;
        }

        public async Task<IEnumerable<Venda>> GetAllAsync()
        {
            return await _context.Vendas
                .Include(v => v.ItensVenda)
                .ThenInclude(iv => iv.Produto)
                .ToListAsync();
        }

        public async Task<Venda?> GetByIdAsync(int id)
        {
            return await _context.Vendas
                .Include(v => v.ItensVenda)
                .ThenInclude(iv => iv.Produto)
                .FirstOrDefaultAsync(v => v.VendaID == id);
        }
    }
}
