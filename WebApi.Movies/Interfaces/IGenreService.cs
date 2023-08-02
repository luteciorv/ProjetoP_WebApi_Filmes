using WebApi.Cinema.DTOs.Genre;

namespace WebApi.Cinema.Interfaces
{
    public interface IGenreService
    {
        Task<IReadOnlyCollection<ReadGenreDto>> GetAllAsync(int skip, int take);
        Task<ReadGenreDto> GetByIdAsync(Guid id);

        Task CreateAsync(CreateGenreDto dto);
        Task UpdateAsync(Guid id, UpdateGenreDto dto);
        Task DeleteAsync(Guid id);
    }
}
