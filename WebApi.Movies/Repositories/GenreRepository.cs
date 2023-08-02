using Microsoft.EntityFrameworkCore;
using WebApi.Cinema.Entities;
using WebApi.Cinema.Interfaces.Repositories;
using WebApi.Movies.Context;

namespace WebApi.Cinema.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(DataContext dataContext) :base(dataContext) { }

        public new async Task<IReadOnlyCollection<Genre>> GetAllAsync(int skip, int take) =>
            await _dataContext.Genres.Skip(skip).Take(take)
                .Include(g => g.MoviesGenres)
                .ThenInclude(mg => mg.Movie)
                .AsNoTracking()
                .ToListAsync();

        public new async Task<Genre?> GetByIdAsync(Guid id) =>
            await _dataContext.Genres
                .Include(g => g.MoviesGenres)
                .ThenInclude(mg => mg.Movie)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
    }
}
