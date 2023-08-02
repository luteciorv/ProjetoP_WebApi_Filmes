using WebApi.Cinema.Entities;

namespace WebApi.Cinema.Interfaces.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        new Task<IReadOnlyCollection<Genre>> GetAllAsync(int skip, int take);

        new Task<Genre?> GetByIdAsync(Guid id);
    }
}
