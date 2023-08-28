using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.Models
{
    public class Assinatura : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int ValorInteiro { get; set; }
        public int? ValorCentavos { get; set; }
        [Required]
        public int DiaVencimento { get; set; }
        public virtual Cartao Cartao { get; set; }
        [Required]
        public int CartaoId { get; set; }
        public virtual List<AssinaturaFatura>? AssinaturasFatura { get; set; }
    }
}
