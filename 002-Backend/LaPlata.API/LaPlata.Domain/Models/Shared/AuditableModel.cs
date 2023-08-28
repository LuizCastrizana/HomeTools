namespace LaPlata.Domain.Models
{
    public class AuditableModel
    {
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
