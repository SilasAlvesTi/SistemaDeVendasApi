using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeVendasApi.Data;
using SistemaDeVendasApi.Models.Cliente;
using SistemaDeVendasApi.Repositories;
using SistemaDeVendasApi.Services;
using System.Numerics;

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
    }
}
