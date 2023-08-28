using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class UpdateCartaoDTO
    {
        [MinLength(1, ErrorMessage = "Valor inválido")]
        [MaxLength(50, ErrorMessage = "Valor inválido")]
        public string? Nome { get; set; }

        [Range(1, 31, ErrorMessage = "Valor inválido")]
        public int? DiaVencimento { get; set; }

        [Range(1, 31, ErrorMessage = "Valor inválido")]
        public int? DiaFechamento { get; set; }
    }
}
