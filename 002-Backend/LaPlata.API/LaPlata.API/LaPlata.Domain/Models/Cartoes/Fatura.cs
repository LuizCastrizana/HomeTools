using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.Models
{
    public class Fatura : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public virtual Cartao Cartao { get; set; }
        [Required]
        public int CartaoId { get; set; }
        [Required]
        public int Mes { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        public DateTime Vencimento { get; set; }
        public decimal? TotalFatura { get; set; }
        public decimal? TotalComprasParceladas { get; set; }
        public decimal? TotalCompras { get; set; }
        public decimal? TotalAssinaturas { get; set; }
        public virtual List<CompraFatura>? ComprasFatura { get; set; }
        public virtual List<AssinaturaFatura>? AssinaturasFatura { get; set; }
    }
}
