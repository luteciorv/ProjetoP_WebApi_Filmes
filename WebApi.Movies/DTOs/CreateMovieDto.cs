namespace WebApi.DTOs.Movie
{
    public class CreateMovieDto
    {
        public CreateMovieDto(string title, string summary, string genre, int year, int durationInMinutes, double rating)
        {
            Title = title;
            Summary = summary;
            Genre = genre;
            Year = year;
            DurationInMinutes = durationInMinutes;
            Rating = rating;
        }

        public Guid Id { get; set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Genre { get; private set; }
        public int Year { get; private set; }
        public int DurationInMinutes { get; private set; }
        public double Rating { get; private set; }
    }
}
