using LaPlata.Domain.Enums;

namespace LaPlata.Domain.Models
{
    public class RespostaServico<T>
    {
        public EnumStatusResposta Status { get; set; }
        public T? Valor { get; set; }
        public string? Mensagem { get; set; }
        public List<string>? Erros { get; set; }

        public RespostaServico()
        {
            Status = EnumStatusResposta.SUCESSO;
        }
    }
}
