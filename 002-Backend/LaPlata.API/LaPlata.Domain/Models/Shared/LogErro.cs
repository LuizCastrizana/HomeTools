using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.Models
{
    public class LogErro : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Servico { get; set; }
        [Required]
        public string Metodo { get; set; }
        [Required]
        public string Mensagem { get; set; }
        public string? DtoJson { get; set; }

        public LogErro()
        {
            Id = 0;
            Servico = string.Empty;
            Metodo = string.Empty;
            Mensagem = string.Empty;
        }

        public LogErro(string servico, string metodo, string mensagem, string? dtojson = null)
        {
            Servico = servico;
            Metodo = metodo;
            Mensagem = mensagem;
            DtoJson = dtojson;
        }
    }
}
