namespace WebApi.Movies.Entity
{
    public sealed class Movie
    {
        public Movie(string title, string summary, string genre, int year, int durationInMinutes, double rating)
        {
            Id = Guid.NewGuid();

            Title = title;
            Summary = summary;
            Genre = genre;
            Year = year;
            DurationInMinutes = durationInMinutes;
            Rating = rating;

            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Genre { get; private set; }
        public int Year { get; private set; }
        public int DurationInMinutes { get; private set; }
        public double Rating { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public void Update(string title, string description, string genre, int year, int durationInMinutes, double rating)
        {
            Title = title;
            Summary = description;
            Genre = genre;
            Year = year;
            DurationInMinutes = durationInMinutes;
            Rating = rating;

            UpdatedAt = DateTime.UtcNow;
        }
    }
}
