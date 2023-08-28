namespace LaPlata.Domain.DTOs
{
    public class ReadFaturaDTO
    {
        public int Id { get; set; }
        public ReadCartaoDTO Cartao{ get; set; }
        public DateTime? Vencimento { get; set; }
        public Decimal? Valor { get; set; }
        public List<CompraFaturaDTO> ComprasFatura { get; set; }
        public List<AssinaturaFaturaDTO> AssinaturasFatura { get; set; }
    }
}
