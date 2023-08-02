using System.ComponentModel.DataAnnotations;

namespace WebApi.Cinema.DTOs.Genre
{
    public record CreateGenreInputModel(
        [Required(ErrorMessage = $"O campo {nameof(Name)} é obrigatório.")]
        [MaxLength(20, ErrorMessage = $"O campo {nameof(Name)} deve ter no máximo 20 caracteres.")]
        string Name
    );
}
