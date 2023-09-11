using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class CreateDespesaDTO
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(1, ErrorMessage = "Valor inválido")]
        [MaxLength(50, ErrorMessage = "Valor inválido")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Valor inválido")]
        public int? ValorInteiro { get; set; }

        [Range(0, 99, ErrorMessage = "Valor inválido")]
        public int? ValorCentavos { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime? DataDespesa { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Valor inválido")]
        public int? QtdParcelas { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Valor inválido")]
        public int? CategoriaId { get; set; }
    }
}
