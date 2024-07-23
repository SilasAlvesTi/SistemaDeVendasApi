namespace SistemaDeVendasApi.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AdicionarAsync(T entidade);
        Task EditarAsync(T entidade);
        Task ApagarAsync(T entidade);
    }
}
