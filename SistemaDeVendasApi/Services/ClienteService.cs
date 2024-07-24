using Microsoft.AspNetCore.Mvc;
using SistemaDeVendasApi.Models.ClienteModels;
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

        public async Task<Cliente> AdicionarClienteAsync(Cliente cliente)
        {
            return await _clienteRepository.AdicionarAsync(cliente);
        }

        public async Task<Cliente> EditarClienteAsync(int id, Cliente cliente)
        {
            return await _clienteRepository.EditarAsync(id, cliente);
        }

        public async Task<Cliente?> GetClienteByIdAsync(int id) 
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public async Task RemoverClienteByIdAsync(int id)
        {
            await _clienteRepository.ApagarAsync(id);
        }
    }
}
