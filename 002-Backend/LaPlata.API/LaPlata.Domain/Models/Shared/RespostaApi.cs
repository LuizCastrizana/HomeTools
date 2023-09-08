using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPlata.Domain.Models
{
    public class RespostaApi<T>
    {
        public T? valor { get; set; }
        public string? mensagem { get; set; }
        public List<string>? erros { get; set; }

        public RespostaApi()
        {
        }

        public RespostaApi(string mensagem)
        {
            this.mensagem = mensagem;
        }
    }
}
