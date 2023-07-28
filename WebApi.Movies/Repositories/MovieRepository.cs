using Microsoft.EntityFrameworkCore;
using WebApi.Movies.Context;
using WebApi.Movies.Entity;
using WebApi.Movies.Interfaces;

namespace WebApi.Movies.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly DataContext _context;

        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Movie>> GetAllAsync(int skip, int take) =>
            await _context.Movies.Skip(skip).Take(take).ToListAsync();

        public async Task<Movie?> GetByIdAsync(Guid id) =>
            await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        public async Task CreateAsync(Movie entity) =>
            await _context.AddAsync(entity);

        public void Delete(Movie entity) =>
            _context.Remove(entity);

        public void Update(Movie entity) =>
            _context.Update(entity);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
