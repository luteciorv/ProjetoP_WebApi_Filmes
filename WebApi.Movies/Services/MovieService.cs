using WebApi.Cinema.DTOs.Movie;
using WebApi.Cinema.Entities;
using WebApi.Cinema.Interfaces.Repositories;
using WebApi.Cinema.Interfaces.Services;
using WebApi.Movies.Exceptions;
using WebApi.Movies.Extensions;

namespace WebApi.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _uow;

        public MovieService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyCollection<ReadMovieDto>> GetAllAsync(int skip, int take)
        {
            var movies = await _uow.Movies.GetAllAsync(skip, take);
            var moviesDto = movies.Select(m => m.MapToReadDto());

            return moviesDto.ToList();
        }

        public async Task<IReadOnlyCollection<ReadMovieDto>> GetAllByGenreAsync(Guid id)
        {
            var movies = await _uow.Movies.GetAllByGenreAsync(id);
            var moviesDto = movies.Select(m => m.MapToReadDto());

            return moviesDto.ToList();
        }

        public async Task<ReadMovieDto> GetByIdAsync(Guid id)
        {
            var movie = await _uow.Movies.GetByIdAsync(id) ?? 
                        throw new EntityNotFoundException($"O filme de id {id} não foi encontrado.");

            return movie.MapToReadDto();
        }

        public async Task CreateAsync(CreateMovieDto dto)
        {
            var movie = dto.MapToMovie();

            await _uow.Movies.CreateAsync(movie);

            foreach (var genreId in dto.GenresId)
            {
                var movieGenre = new MovieGenre(movie.Id, genreId);
                await _uow.MoviesGenres.CreateAsync(movieGenre);
            }

            await _uow.CommitAsync();

            dto.Id = movie.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var movie = await _uow.Movies.GetByIdAsync(id) ?? 
                        throw new EntityNotFoundException($"O filme de id {id} não foi encontrado.");

            _uow.Movies.Delete(movie);
            await _uow.CommitAsync();
        }

        public async Task UpdateAsync(Guid id, UpdateMovieDto dto)
        {
            var movie = await _uow.Movies.GetByIdAsync(id) ?? 
                        throw new EntityNotFoundException($"O filme de id {id} não foi encontrado.");

            movie.Update(dto.Title, dto.Summary, dto.Genre, dto.Year, dto.DurationInMinutes, dto.Rating);

            _uow.Movies.Update(movie);
            await _uow.CommitAsync();
        }
    }
}
