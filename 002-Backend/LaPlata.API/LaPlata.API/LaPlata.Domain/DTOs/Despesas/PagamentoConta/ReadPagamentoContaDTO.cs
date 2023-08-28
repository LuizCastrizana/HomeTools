namespace LaPlata.Domain.DTOs
{
    public class ReadPagamentoContaDTO
    {
        public int Id { get; set; }
        public DateTime DataPagamento { get; set; }
        public ReadContaDTO Conta { get; set; }
    }
}
