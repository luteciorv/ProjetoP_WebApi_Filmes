using WebApi.Movies.DTOs;
using WebApi.Movies.Entity;

namespace WebApi.Movies.Extensions
{
    public static class MovieMap
    {
        public static Movie MapToMovie(this CreateMovieDto dto) =>
            new(dto.Title, dto.Summary, dto.Genre, dto.Year, dto.DurationInMinutes, dto.Rating);

        public static Movie MapToMovie(this UpdateMovieDto dto) =>
            new(dto.Title, dto.Summary, dto.Genre, dto.Year, dto.DurationInMinutes, dto.Rating);

        public static ReadMovieDto MapToReadDto(this Movie entity) =>
            new(entity.Id, entity.Title, entity.Summary, entity.Genre, entity.Year, entity.DurationInMinutes, entity.Rating);

        public static CreateMovieDto MapToCreateMovieDto(this CreateMovieInputModel inputModel) =>
            new(inputModel.Title, inputModel.Summary, inputModel.Genre, inputModel.Year, inputModel.DurationInMinutes, inputModel.Rating);
    }
}
