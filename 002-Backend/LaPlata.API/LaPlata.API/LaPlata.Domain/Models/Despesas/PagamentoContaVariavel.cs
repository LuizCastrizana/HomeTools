using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaPlata.Domain.Models
{
    public class PagamentoContaVariavel : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ValorInteiro { get; set; }
        public int? ValorCentavos { get; set; }
        [Required]
        public DateTime DataPagamento { get; set; }
        [JsonIgnore]
        public virtual ContaVariavel ContaVariavel { get; set; }
        [Required]
        public int ContaVariavelId { get; set; }
        [JsonIgnore]
        public virtual Compra? Compra { get; set; }
        public int? CompraId { get; set; }
    }
}
