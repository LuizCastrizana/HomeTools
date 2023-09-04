namespace LaPlata.Domain.DTOs
{
    public class ReadPagamentoContaVariavelDTO
    {
        public int Id { get; set; }
        public int ValorInteiro { get; set; }
        public int? ValorCentavos { get; set; }
        public DateTime DataPagamento { get; set; }
        public int ContaVariavelId { get; set; }
    }
}
