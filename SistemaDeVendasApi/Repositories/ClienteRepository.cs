using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaDeVendasApi.Data;
using SistemaDeVendasApi.Models.Cliente;
using System;

namespace SistemaDeVendasApi.Repositories
{
    public class ClienteRepository : IRepository<Cliente>
    {

        private readonly SistemaDeVendasContext _context;

        public ClienteRepository(SistemaDeVendasContext context)
        {
            _context = context;
        }

        public async Task<Cliente> AdicionarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task ApagarAsync(int id)
        {
            var clienteDoBanco = await _context.Clientes.Where(cliente => cliente.Id == id).FirstAsync();
            if (clienteDoBanco is null)
            {
                return;
            }
            _context.Clientes.Remove(clienteDoBanco);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> EditarAsync(int id, Cliente cliente)
        {
            var clienteDoBanco = await _context.Clientes.Where(cliente => cliente.Id == id).FirstAsync();

            clienteDoBanco.Nome = cliente.Nome;
            
            await _context.SaveChangesAsync();

            return clienteDoBanco;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes.Where((Cliente cliente) => cliente.Id == id).FirstAsync();
        }
    }
}
