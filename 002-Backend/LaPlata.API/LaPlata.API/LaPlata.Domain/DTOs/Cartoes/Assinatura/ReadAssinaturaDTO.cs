namespace LaPlata.Domain.DTOs
{
    public class ReadAssinaturaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int ValorInteiro { get; set; }
        public int ValorCentavos { get; set; }
        public int DiaVencimento { get; set; }
        public int CartaoId { get; set; }
    }
}
