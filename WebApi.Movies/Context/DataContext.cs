using Microsoft.EntityFrameworkCore;
using WebApi.Movies.Entity;

namespace WebApi.Movies.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
