namespace LaPlata.Domain.DTOs
{
    public class ReadCompraDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int ValorInteiro { get; set; }
        public int ValorCentavos { get; set; }
        public DateTime DataCompra { get; set; }
        public int QtdParcelas { get; set; }
        public bool Paga { get; set; }
        public int CartaoId { get; set; }
        public ReadCategoriaDTO Categoria { get; set; }
    }
}
