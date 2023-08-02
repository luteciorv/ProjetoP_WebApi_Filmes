namespace WebApi.DTOs.Movie
{
    public record ReadMovieDto(Guid Id, string Title, string Summary, string Genre, int Year, int DurationInMinutes, double Rating);
}
