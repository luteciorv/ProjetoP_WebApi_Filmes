using WebApi.Cinema.Interfaces.Repositories;
using WebApi.Movies.Context;

namespace WebApi.Cinema.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMovieRepository Movies { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IMovieGenreRepository MoviesGenres { get; private set; }

        private readonly DataContext _dataContext;

        public UnitOfWork(
            IMovieRepository movieRepository,
            IGenreRepository genreRepository,
            IMovieGenreRepository movieGenreRepository,
            DataContext dataContext)
        {
            Movies = movieRepository;
            Genres = genreRepository;
            MoviesGenres = movieGenreRepository;

            _dataContext = dataContext;
        }

        public async Task CommitAsync() =>
             await _dataContext.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) _dataContext.Dispose();
        }
    }
}
