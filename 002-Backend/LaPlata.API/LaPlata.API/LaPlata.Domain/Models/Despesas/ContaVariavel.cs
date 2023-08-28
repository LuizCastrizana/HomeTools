using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaPlata.Domain.Models
{
    public class ContaVariavel : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int DiaVencimento { get; set; }
        public virtual Categoria Categoria { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public virtual List<PagamentoContaVariavel>? Pagamentos { get; set; }
    }
}
