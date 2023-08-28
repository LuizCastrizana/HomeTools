namespace LaPlata.Domain.DTOs
{
    public class CompraFaturaDTO
    {
        public ReadCompraDTO Compra { get; set; }
        public int Parcela { get; set; }
        public Decimal ValorParcela { get; set; }
    }
}
