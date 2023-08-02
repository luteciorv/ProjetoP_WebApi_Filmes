using System.ComponentModel.DataAnnotations;

namespace WebApi.Cinema.DTOs.Genre
{
    public record UpdateGenreDto(
        [Required(ErrorMessage = $"O campo {nameof(Name)} é obrigatório.")]
        [MaxLength(20, ErrorMessage = $"O campo {nameof(Name)} deve ter no máximo 20 caracteres.")]
        string Name
    );
}
