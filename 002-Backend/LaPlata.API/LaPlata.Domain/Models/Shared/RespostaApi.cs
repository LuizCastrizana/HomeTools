namespace LaPlata.Domain.Models
{
    public class RespostaApi<T>
    {
        public T? valor { get; set; }
        public string? mensagem { get; set; }
        public List<string>? erros { get; set; }
        public int status { get; set; }

        public RespostaApi()
        {
        }

        public RespostaApi(string mensagem)
        {
            this.mensagem = mensagem;
        }
    }
}
