namespace LaPlata.Domain.DTOs
{
    public class ReadDespesaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int ValorInteiro { get; set; }
        public int? ValorCentavos { get; set; }
        public DateTime DataDespesa { get; set; }
        public int? QtdParcelas { get; set; }
        public ReadCategoriaDTO Categoria { get; set; }
    }
}
