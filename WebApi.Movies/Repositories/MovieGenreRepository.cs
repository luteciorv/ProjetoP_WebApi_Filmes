using WebApi.Cinema.Entities;
using WebApi.Cinema.Interfaces.Repositories;
using WebApi.Movies.Context;

namespace WebApi.Cinema.Repositories
{
    public class MovieGenreRepository : Repository<MovieGenre>, IMovieGenreRepository
    {
        public MovieGenreRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
