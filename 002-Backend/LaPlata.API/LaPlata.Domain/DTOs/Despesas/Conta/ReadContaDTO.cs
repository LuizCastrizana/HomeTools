namespace LaPlata.Domain.DTOs
{
    public class ReadContaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int ValorInteiro { get; set; }
        public int? ValorCentavos { get; set; }
        public int DiaVencimento { get; set; }
        public ReadCategoriaDTO Categoria { get; set; }
        public List<ReadPagamentoContaDTO> Pagamentos { get; set; }
    }
}
