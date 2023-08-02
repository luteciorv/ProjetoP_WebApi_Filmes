using WebApi.Cinema.DTOs.Genre;
using WebApi.Cinema.Entity;
using WebApi.Cinema.Extensions;
using WebApi.Cinema.Interfaces;
using WebApi.Movies.Exceptions;
using WebApi.Movies.Interfaces;

namespace WebApi.Cinema.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _repository;

        public GenreService(IRepository<Genre> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ReadGenreDto>> GetAllAsync(int skip, int take)
        {
            var genres = await _repository.GetAllAsync(skip, take);
            return genres.Select(g => g.MapToReadGenreDto()).ToList();
        }

        public async Task<ReadGenreDto> GetByIdAsync(Guid id)
        {
            var genre = await _repository.GetByIdAsync(id) ?? throw new EntityNotFoundException($"O gênero de id {id} não foi encontrado.");
            var genreDto = genre.MapToReadGenreDto();

            return genreDto;
        }

        public async Task CreateAsync(CreateGenreDto dto)
        {
            var genre = dto.MapToGenre();

            await _repository.CreateAsync(genre);
            await _repository.SaveAsync();

            dto.Id = genre.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateGenreDto dto)
        {
            var genre = await _repository.GetByIdAsync(id) ?? throw new EntityNotFoundException($"O gênero de id {id} não foi encontrado.");

            genre.Update(dto.Name);

            _repository.Update(genre);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var genre = await _repository.GetByIdAsync(id) ?? throw new EntityNotFoundException($"O gênero de id {id} não foi encontrado.");

            _repository.Delete(genre);
            await _repository.SaveAsync();
        }
    }
}