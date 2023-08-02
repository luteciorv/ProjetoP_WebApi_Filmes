using WebApi.DTOs.Movie;
using WebApi.Movies.Entity;
using WebApi.Movies.Exceptions;
using WebApi.Movies.Extensions;
using WebApi.Movies.Interfaces;

namespace WebApi.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _repository;

        public MovieService(IRepository<Movie> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ReadMovieDto>> GetAllAsync(int skip, int take)
        {
            var movies = await _repository.GetAllAsync(skip, take);
            var moviesDto = movies.Select(m => m.MapToReadDto());

            return moviesDto.ToList();
        }

        public async Task<ReadMovieDto> GetByIdAsync(Guid id)
        {
            var movie = await _repository.GetByIdAsync(id) ?? throw new EntityNotFoundException($"O filme de id {id} não foi encontrado.");

            return movie.MapToReadDto();
        }

        public async Task CreateAsync(CreateMovieDto dto)
        {
            var movie = dto.MapToMovie();

            await _repository.CreateAsync(movie);
            await _repository.SaveAsync();

            dto.Id = movie.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var movie = await _repository.GetByIdAsync(id) ?? throw new EntityNotFoundException($"O filme de id {id} não foi encontrado.");

            _repository.Delete(movie);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(Guid id, UpdateMovieDto dto)
        {
            var movie = await _repository.GetByIdAsync(id) ?? throw new EntityNotFoundException($"O filme de id {id} não foi encontrado.");
            
            movie.Update(dto.Title, dto.Summary, dto.Genre, dto.Year, dto.DurationInMinutes, dto.Rating);

            _repository.Update(movie);
            await _repository.SaveAsync();
        }
    }
}
