using Microsoft.AspNetCore.Mvc;
using SistemaDeVendasApi.Models.ClienteModels;
using SistemaDeVendasApi.Services;

namespace SistemaDeVendasApi.EndPoints.ClienteEndPoints
{
    [Route("/clientes")]
    [ApiController]
    public class ClienteEndPoints : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteEndPoints(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetAllClientesAsync();
            return new ActionResult<IEnumerable<Cliente>>(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente is null)
            {
                return new NotFoundResult();
            }
            return new ActionResult<Cliente>(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
        {
            var clienteReadDto = await _clienteService.AdicionarClienteAsync(cliente);
            return new ActionResult<Cliente>(cliente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> PutCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return new BadRequestResult();
            }
            var clienteReadDto = await _clienteService.EditarClienteAsync(id, cliente);
            return new ActionResult<Cliente>(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
           await _clienteService.RemoverClienteByIdAsync(id);
            return NoContent();
        }

    }
}
