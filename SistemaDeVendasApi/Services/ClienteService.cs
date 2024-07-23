using Microsoft.AspNetCore.Mvc;
using SistemaDeVendasApi.Models.Cliente;
using SistemaDeVendasApi.Repositories;

namespace SistemaDeVendasApi.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task AdicionarClienteAsync(Cliente cliente)
        {
            await _clienteRepository.AdicionarAsync(cliente);
        }

        public async Task EditarClienteAsync(Cliente cliente)
        {
            await _clienteRepository.EditarAsync(cliente);
        }

        public async Task<Cliente?> GetClienteByIdAsync(int id) 
        {
            return await _clienteRepository.GetByIdAsync(id);
        }
    }
}
