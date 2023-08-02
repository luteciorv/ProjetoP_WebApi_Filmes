namespace WebApi.Cinema.DTOs.Genre
{
    public class CreateGenreDto
    {
        public CreateGenreDto(string name)
        {
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; private set; }
    }
}
