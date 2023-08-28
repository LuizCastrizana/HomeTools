using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.Models
{
    public class Despesa : AuditableModel
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
        public DateTime DataDespesa { get; set; }
        public int? QtdParcelas { get; set; }
        public virtual Categoria Categoria { get; set; }
        [Required]
        public int CategoriaId { get; set; }
    }
}
