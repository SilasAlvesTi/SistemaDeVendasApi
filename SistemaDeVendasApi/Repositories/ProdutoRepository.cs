using Microsoft.EntityFrameworkCore;
using SistemaDeVendasApi.Data;
using SistemaDeVendasApi.Models.ProdutoModels;

namespace SistemaDeVendasApi.Repositories
{
    public class ProdutoRepository : IRepository<Produto>
    {

        private readonly SistemaDeVendasContext _context;

        public ProdutoRepository(SistemaDeVendasContext context)
        {
            _context = context;
        }

        public async Task<Produto> AdicionarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task ApagarAsync(int id)
        {
            var produtoDoBanco = await _context.Produtos.Where(produto => produto.Id == id).FirstAsync();
            if (produtoDoBanco is null)
            {
                return;
            }
            _context.Produtos.Remove(produtoDoBanco);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> EditarAsync(int id, Produto produto)
        {
            var produtoDoBanco = await _context.Produtos.Where(produto => produto.Id == id).FirstAsync();

            produtoDoBanco.Nome = produto.Nome;
            produtoDoBanco.Preco = produto.Preco;
            produtoDoBanco.Estoque = produto.Estoque;

            await _context.SaveChangesAsync();

            return produtoDoBanco;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            return await _context.Produtos.Where((Produto produto) => produto.Id == id).FirstAsync();
        }
    }
}
