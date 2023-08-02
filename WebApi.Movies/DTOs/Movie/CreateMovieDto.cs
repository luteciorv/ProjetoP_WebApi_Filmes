namespace WebApi.Cinema.DTOs.Movie
{
    public class CreateMovieDto
    {
        public CreateMovieDto(string title, string summary, string genre, int year, int durationInMinutes, double rating, List<Guid> genresId)
        {
            Title = title;
            Summary = summary;
            Genre = genre;
            Year = year;
            DurationInMinutes = durationInMinutes;
            Rating = rating;

            GenresId = genresId;
        }

        public Guid Id { get; set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Genre { get; private set; }
        public int Year { get; private set; }
        public int DurationInMinutes { get; private set; }
        public double Rating { get; private set; }
        public List<Guid> GenresId { get; private set; }
    }
}
