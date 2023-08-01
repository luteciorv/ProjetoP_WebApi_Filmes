using System.ComponentModel.DataAnnotations;

namespace WebApi.Theaters.DTOs.Movie
{
    public record UpdateMovieDto
    {
        [Required(ErrorMessage = $"O campo {nameof(Title)} é obrigatório.")]
        [StringLength(75, ErrorMessage = $"O campo {nameof(Title)} precisa conter no máximo 75 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = $"O campo {nameof(Summary)} é obrigatório.")]
        [StringLength(200, ErrorMessage = $"O campo {nameof(Summary)} precisa conter no máximo 200 caracteres.")]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = $"O campo {nameof(Genre)} é obrigatório.")]
        [StringLength(15, ErrorMessage = $"O campo {nameof(Genre)} precisa conter no máximo 15 caracteres.")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = $"O campo {nameof(Year)} é obrigatório.")]
        [Range(1900, 3000, ErrorMessage = $"O campo {nameof(Year)} precisa conter exatamente 4 caracteres.")]
        public int Year { get; set; }

        [Required(ErrorMessage = $"O campo {nameof(DurationInMinutes)} é obrigatório.")]
        [Range(30, 900, ErrorMessage = $"O campo {nameof(DurationInMinutes)} precisa conter de 2 a 3 caracteres.")]
        public int DurationInMinutes { get; set; }

        [Required(ErrorMessage = $"O campo {nameof(Rating)} é obrigatório.")]
        public double Rating { get; set; }
    }
}
