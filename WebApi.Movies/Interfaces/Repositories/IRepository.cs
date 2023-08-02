using WebApi.Cinema.Entities;

namespace WebApi.Cinema.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(int skip, int take);
        Task<TEntity?> GetByIdAsync(Guid id);

        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
