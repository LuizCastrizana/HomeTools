using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.Models
{
    public class Cartao : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public int DiaVencimento { get; set; }
        [Required]
        public int DiaFechamento { get; set; }
        public virtual List<Compra>? Compras { get; set; }
        public virtual List<Assinatura>? Assinaturas { get; set; }
        public virtual List<Fatura>? Faturas { get; set; }
    }
}
