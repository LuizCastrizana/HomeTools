using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaPlata.Domain.Models
{
    public class PagamentoConta : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime DataPagamento { get; set; }
        public virtual Conta Conta { get; set; }
        [Required]
        public int ContaId { get; set; }
        [JsonIgnore]
        public virtual Compra? Compra { get; set; }
        public int? CompraId { get; set; }
    }
}
