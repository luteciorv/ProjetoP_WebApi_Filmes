using WebApi.Cinema.DTOs.Genre;
using WebApi.Cinema.Entities;

namespace WebApi.Cinema.Extensions
{
    public static class GenreMap
    {
        public static CreateGenreDto MapToCreateGenreDto(this CreateGenreInputModel inputModel) =>
            new(inputModel.Name);

        public static Genre MapToGenre(this CreateGenreDto dto) =>
            new(dto.Name);

        public static Genre MapToGenre(this UpdateGenreDto dto) =>
            new(dto.Name);

        public static ReadGenreDto MapToReadGenreDto(this Genre entity) =>
            new(entity.Id, entity.Name);
    }
}
