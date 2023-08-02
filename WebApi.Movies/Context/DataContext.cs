using Microsoft.EntityFrameworkCore;
using WebApi.Cinema.Entity;
using WebApi.Movies.Entity;

namespace WebApi.Movies.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
