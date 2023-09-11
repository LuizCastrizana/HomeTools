using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class UpdatePagamentoContaDTO
    {
        public DateTime? DataPagamento { get; set; }
        public int? CompraId { get; set; }
        [Range(1, 12, ErrorMessage = "Valor inválido")]
        public int? MesReferencia { get; set; }
        [Range(1, 9999, ErrorMessage = "Valor inválido")]
        public int? AnoReferencia { get; set; }
    }
}
