using SistemaDeVendasApi.Models.ProdutoModels;
using SistemaDeVendasApi.Repositories;

namespace SistemaDeVendasApi.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoService(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<Produto> AdicionarProdutoAsync(Produto produto)
        {
            return await _produtoRepository.AdicionarAsync(produto);
        }

        public async Task<Produto> EditarProdutoAsync(int id, Produto produto)
        {
            return await _produtoRepository.EditarAsync(id, produto);
        }

        public async Task<Produto?> GetProdutoByIdAsync(int id)
        {
            return await _produtoRepository.GetByIdAsync(id);
        }

        public async Task RemoverProdutoByIdAsync(int id)
        {
            await _produtoRepository.ApagarAsync(id);
        }

    }
}
