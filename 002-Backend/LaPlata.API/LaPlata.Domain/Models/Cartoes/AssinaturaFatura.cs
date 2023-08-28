using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.Models
{
    public class AssinaturaFatura : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public virtual Fatura Fatura { get; set; }
        [Required]
        public int FaturaId { get; set; }
        public virtual Assinatura Assinatura { get; set; }
        [Required]
        public int AssinaturaId { get; set; }
    }
}
