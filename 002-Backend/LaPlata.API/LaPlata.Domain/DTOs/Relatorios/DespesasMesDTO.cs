using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class DespesasMesDTO
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, 12, ErrorMessage = "Valor inválido")]
        public int? Mes { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, 9999, ErrorMessage = "Valor inválido")]
        public int? Ano { get; set; }
        public decimal TotalMes { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal TotalPagamentosConta { get; set; }
        public decimal TotalPagamentosContaVariavel { get; set; }
        public decimal TotalFaturas { get; set; }
        public List<ReadDespesaDTO> Desesas { get; set; }
        public List<ReadPagamentoContaDTO> PagamntosConta { get; set; }
        public List<ReadPagamentoContaVariavelDTO> PagamentosContaVariavel { get; set; }
        public List<ReadFaturaDTO> Faturas { get; set; }
    }
}
