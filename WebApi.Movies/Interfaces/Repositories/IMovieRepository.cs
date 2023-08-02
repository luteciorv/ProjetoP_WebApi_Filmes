using WebApi.Movies.Entity;

namespace WebApi.Cinema.Interfaces.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        new Task<IReadOnlyCollection<Movie>> GetAllAsync(int skip, int take);
        new Task<Movie?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<Movie>> GetAllByGenreAsync(Guid id);
    }
}
