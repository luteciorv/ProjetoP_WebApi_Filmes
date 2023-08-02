using Microsoft.EntityFrameworkCore;
using WebApi.Cinema.Interfaces.Repositories;
using WebApi.Cinema.Repositories;
using WebApi.Movies.Context;
using WebApi.Movies.Entity;

namespace WebApi.Movies.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DataContext dataContext) : base(dataContext) { }

        public new async Task<IReadOnlyCollection<Movie>> GetAllAsync(int skip, int take) =>
            await _dataContext.Movies.Skip(skip).Take(take)
                .Include(m => m.MoviesGenres)
                .ThenInclude(mg => mg.Genre)
                .AsNoTracking()
                .ToListAsync();

        public new async Task<Movie?> GetByIdAsync(Guid id) =>
            await _dataContext.Movies
                .Include(m => m.MoviesGenres)
                .ThenInclude(mg => mg.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<IReadOnlyCollection<Movie>> GetAllByGenreAsync(Guid id) =>
            await _dataContext.Movies
                .Include(m => m.MoviesGenres)
                .ThenInclude(mg => mg.Genre)
                .Where(m => m.MoviesGenres.Any(mg => mg.GenreId == id))
                .AsNoTracking()
                .ToListAsync();
    }
}
