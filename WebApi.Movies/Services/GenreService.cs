using WebApi.Cinema.DTOs.Genre;
using WebApi.Cinema.Extensions;
using WebApi.Cinema.Interfaces.Repositories;
using WebApi.Cinema.Interfaces.Services;
using WebApi.Movies.Exceptions;

namespace WebApi.Cinema.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _uow;

        public GenreService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyCollection<ReadGenreDto>> GetAllAsync(int skip, int take)
        {
            var genres = await _uow.Genres.GetAllAsync(skip, take);
            var genresDto = genres.Select(g => g.MapToReadGenreDto());

            return genresDto.ToList();
        }

        public async Task<ReadGenreDto> GetByIdAsync(Guid id)
        {
            var genre = await _uow.Genres.GetByIdAsync(id) ?? 
                        throw new EntityNotFoundException($"O gênero de id {id} não foi encontrado.");
            var genreDto = genre.MapToReadGenreDto();

            return genreDto;
        }

        public async Task CreateAsync(CreateGenreDto dto)
        {
            var genre = dto.MapToGenre();

            await _uow.Genres.CreateAsync(genre);
            await _uow.CommitAsync();

            dto.Id = genre.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateGenreDto dto)
        {
            var genre = await _uow.Genres.GetByIdAsync(id) ?? 
                        throw new EntityNotFoundException($"O gênero de id {id} não foi encontrado.");

            genre.Update(dto.Name);

            _uow.Genres.Update(genre);
            await _uow.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var genre = await _uow.Genres.GetByIdAsync(id) ?? 
                        throw new EntityNotFoundException($"O gênero de id {id} não foi encontrado.");

            _uow.Genres.Delete(genre);
            await _uow.CommitAsync();
        }
    }
}