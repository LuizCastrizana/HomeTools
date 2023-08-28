using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPlata.Domain.Models
{
    public class RespostaApi<T>
    {
        public T? Valor { get; set; }
        public string? Mensagem { get; set; }
        public List<string>? Erros { get; set; }

        public RespostaApi()
        {
        }

        public RespostaApi(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}
