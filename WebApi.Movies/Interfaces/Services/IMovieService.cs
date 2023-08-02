using WebApi.Cinema.DTOs.Movie;

namespace WebApi.Cinema.Interfaces.Services
{
    public interface IMovieService
    {
        Task<IReadOnlyCollection<ReadMovieDto>> GetAllAsync(int skip, int take);
        Task<IReadOnlyCollection<ReadMovieDto>> GetAllByGenreAsync(Guid id);
        Task<ReadMovieDto> GetByIdAsync(Guid id);

        Task CreateAsync(CreateMovieDto dto);
        Task UpdateAsync(Guid id, UpdateMovieDto dto);
        Task DeleteAsync(Guid id);
    }
}
