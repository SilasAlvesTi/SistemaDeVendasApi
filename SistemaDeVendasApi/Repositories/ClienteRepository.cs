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

        public Task AdicionarAsync(Cliente entidade)
        {
            throw new NotImplementedException();
        }

        public Task ApagarAsync(Cliente entidade)
        {
            throw new NotImplementedException();
        }

        public Task EditarAsync(Cliente entidade)
        {
            throw new NotImplementedException();
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
