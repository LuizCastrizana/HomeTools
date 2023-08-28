using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs.ResumoMensal
{
    public class CreateDespesasMesDTO
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, 12, ErrorMessage = "Valor inválido")]
        public int? Mes { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, 9999, ErrorMessage = "Valor inválido")]
        public int? Ano { get; set; }
    }
}
