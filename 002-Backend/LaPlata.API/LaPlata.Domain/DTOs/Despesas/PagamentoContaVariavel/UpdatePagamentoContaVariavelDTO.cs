using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class UpdatePagamentoContaVariavelDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Valor inválido")]
        public int? ValorInteiro { get; set; }

        [Range(0, 99, ErrorMessage = "Valor inválido")]
        public int? ValorCentavos { get; set; }
        public DateTime? DataPagamento { get; set; }
        [Range(1, 12, ErrorMessage = "Valor inválido")]
        public int? MesReferencia { get; set; }
        [Range(1, 9999, ErrorMessage = "Valor inválido")]

        public int? AnoReferencia { get; set; }
    }
}
