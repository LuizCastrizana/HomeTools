namespace LaPlata.Domain.Models.Comum
{
    public class ResultadoValidacao
    {
        public bool Valido { get; set; }
        public List<string> Mensagens { get; set; }

        public ResultadoValidacao()
        {
            Valido = true;
            Mensagens = new List<string>();
        }

        public ResultadoValidacao(bool valido)
        {
            Valido = valido;
            Mensagens = new List<string>();
        }

        public ResultadoValidacao(bool valido, List<string> mensagens)
        {
            Valido = valido;
            Mensagens = mensagens;
        }
    }
}
