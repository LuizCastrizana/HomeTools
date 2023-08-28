using LaPlata.Domain.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class CreatePagamentoContaDTO
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DataAnterior(ErrorMessage = "Valor inválido")]
        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? ContaId { get; set; }
        public int? CompraId { get; set; }
    }
}
