using Microsoft.EntityFrameworkCore;
using WebApi.Cinema.Interfaces.Repositories;
using WebApi.Movies.Context;

namespace WebApi.Cinema.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(int skip, int take) =>
            await _dataContext.Set<TEntity>().Skip(skip).Take(take).AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Guid id) =>
            await _dataContext.Set<TEntity>().FindAsync(id);


        public async Task CreateAsync(TEntity entity) =>
            await _dataContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) =>
            _dataContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) =>
            _dataContext.Set<TEntity>().Update(entity);
    }
}
