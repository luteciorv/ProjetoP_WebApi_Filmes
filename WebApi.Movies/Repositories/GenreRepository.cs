using Microsoft.EntityFrameworkCore;
using WebApi.Cinema.Entity;
using WebApi.Movies.Context;
using WebApi.Movies.Interfaces;

namespace WebApi.Cinema.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Genre>> GetAllAsync(int skip, int take) =>
            await _context.Genres.Skip(skip).Take(take).AsNoTracking().ToListAsync();

        public async Task<Genre?> GetByIdAsync(Guid id) =>
            await _context.Genres.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);

        public async Task CreateAsync(Genre entity) =>
            await _context.Genres.AddAsync(entity);

        public void Update(Genre entity) =>
            _context.Genres.Update(entity);

        public void Delete(Genre entity) =>
            _context.Genres.Remove(entity);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
