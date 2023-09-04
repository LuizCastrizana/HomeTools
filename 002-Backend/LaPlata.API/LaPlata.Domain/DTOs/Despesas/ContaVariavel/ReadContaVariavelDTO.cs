namespace LaPlata.Domain.DTOs
{
    public class ReadContaVariavelDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int DiaVencimento { get; set; }
        public ReadCategoriaDTO Categoria { get; set; }
        public List<ReadPagamentoContaVariavelDTO> Pagamentos { get; set; }
    }
}
