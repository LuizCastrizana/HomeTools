using LaPlata.Domain.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class CreatePagamentoContaVariavelDTO
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Valor inválido")]
        public int? ValorInteiro { get; set; }

        [Range(0, 99, ErrorMessage = "Valor inválido")]
        public int? ValorCentavos { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [DataAnterior(ErrorMessage = "Valor inválido")]
        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? ContaVariavelId { get; set; }
    }
}
