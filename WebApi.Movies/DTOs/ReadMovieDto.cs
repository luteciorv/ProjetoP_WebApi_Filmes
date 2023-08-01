using WebApi.Theaters.DTOs.Session;

namespace WebApi.Theaters.DTOs.Movie
{
    public record ReadMovieDto(Guid Id, string Title, string Summary, string Genre, int Year, int DurationInMinutes, double Rating, ICollection<ReadSessionDto>? Sessions);
}
