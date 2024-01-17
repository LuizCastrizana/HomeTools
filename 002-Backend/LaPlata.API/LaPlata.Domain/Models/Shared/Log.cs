using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.Models
{
    public class Log : BaseModel
    {
        [Required]
        public string Servico { get; set; }
        [Required]
        public string Metodo { get; set; }
        [Required]
        public string Mensagem { get; set; }
        public string? DtoJson { get; set; }

        public Log()
        {
            Id = 0;
            Servico = string.Empty;
            Metodo = string.Empty;
            Mensagem = string.Empty;
        }

        public Log(string servico, string metodo, string mensagem, string? dtojson = null)
        {
            Servico = servico;
            Metodo = metodo;
            Mensagem = mensagem;
            DtoJson = dtojson;
        }
    }
}
