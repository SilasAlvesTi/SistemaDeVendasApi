namespace SistemaDeVendasApi.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AdicionarAsync(T entidade);
        Task<T> EditarAsync(int id, T entidade);
        Task ApagarAsync(int id);
    }
}
