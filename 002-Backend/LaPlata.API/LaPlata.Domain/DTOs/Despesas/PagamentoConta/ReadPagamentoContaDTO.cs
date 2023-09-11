using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class ReadPagamentoContaDTO
    {
        public int Id { get; set; }
        public DateTime DataPagamento { get; set; }
        public int ContaId { get; set; }
        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }

    }
}
