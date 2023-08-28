namespace LaPlata.Domain.DTOs
{
    public class ReadCartaoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int DiaVencimento { get; set; }
        public int DiaFechamento { get; set; }
    }
}
