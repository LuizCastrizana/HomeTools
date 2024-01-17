using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaPlata.Domain.Models
{
    public class PagamentoContaVariavel : BaseModel
    {
        [Required]
        public int ValorInteiro { get; set; }
        public int? ValorCentavos { get; set; }
        [Required]
        public DateTime DataPagamento { get; set; }
        [JsonIgnore]
        public virtual ContaVariavel ContaVariavel { get; set; }
        [Required]
        public int ContaVariavelId { get; set; }
        [Required]
        public int MesReferencia { get; set; }
        [Required]
        public int AnoReferencia { get; set; }
        [JsonIgnore]
        public virtual Compra? Compra { get; set; }
        public int? CompraId { get; set; }
    }
}
