using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaPlata.Domain.Models
{
    public class Categoria : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [JsonIgnore]
        public virtual List<Conta> Contas { get; set; }
        [JsonIgnore]
        public virtual List<ContaVariavel> ContasVariaveis { get; set; }
        [JsonIgnore]
        public virtual List<Despesa> Despesas { get; set; }
        [JsonIgnore]
        public virtual List<Compra> Compras { get; set; }
    }
}
