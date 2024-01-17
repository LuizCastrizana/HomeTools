using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaPlata.Domain.Models
{
    public class PagamentoConta : BaseModel
    {
        [Required]
        public DateTime DataPagamento { get; set; }
        public virtual Conta Conta { get; set; }
        [Required]
        public int ContaId { get; set; }
        [Required]
        public int MesReferencia { get; set; }
        [Required]
        public int AnoReferencia { get; set; }
        [JsonIgnore]
        public virtual Compra? Compra { get; set; }
        public int? CompraId { get; set; }
    }
}
