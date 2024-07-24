using Microsoft.AspNetCore.Mvc;
using SistemaDeVendasApi.Models.ProdutoModels;
using SistemaDeVendasApi.Services;

namespace SistemaDeVendasApi.Controllers.ProdutoControllers
{
    [Route("/produtos")]
    [ApiController]
    public class ProdutoEndPoints : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoEndPoints(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return new ActionResult<IEnumerable<Produto>>(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto is null)
            {
                return new NotFoundResult();
            }
            return new ActionResult<Produto>(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto([FromBody] Produto produto)
        {
            var produtoDoBanco = await _produtoService.AdicionarProdutoAsync(produto);
            return new ActionResult<Produto>(produtoDoBanco);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> PutProduto(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
            {
                return new BadRequestResult();
            }
            var produtodoBanco = await _produtoService.EditarProdutoAsync(id, produto);
            return new ActionResult<Produto>(produto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            await _produtoService.RemoverProdutoByIdAsync(id);
            return NoContent();
        }
    }
}

