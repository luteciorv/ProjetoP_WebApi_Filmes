namespace WebApi.Movies.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);

        Task CreateAsync(TEntity entity);   
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
