using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.Models
{
    public class Compra : ModelBase
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int ValorInteiro { get; set; }
        public int? ValorCentavos { get; set; }
        [Required]
        public DateTime DataCompra { get; set; }
        [Required]
        public int QtdParcelas { get; set; }
        public bool? Paga { get; set; }
        public virtual Cartao Cartao { get; set; }
        [Required]
        public int CartaoId { get; set; }
        public virtual Categoria Categoria { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        public virtual List<CompraFatura>? ComprasFatura { get; set; }
        public virtual PagamentoConta? PagamentoConta { get; set; }
        public virtual PagamentoContaVariavel? PagamentoContaVariavel { get; set; }
    }
}
