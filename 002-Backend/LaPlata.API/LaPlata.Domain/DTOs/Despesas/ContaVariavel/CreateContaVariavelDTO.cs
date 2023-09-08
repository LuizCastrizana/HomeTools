using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class CreateContaVariavelDTO
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(1, ErrorMessage = "Valor inválido")]
        [MaxLength(50, ErrorMessage = "Valor inválido")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, 31, ErrorMessage = "Valor inválido")]
        public int? DiaVencimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Valor inválido")]
        public int? CategoriaId { get; set; }
    }
}
