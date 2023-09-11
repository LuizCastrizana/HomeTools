using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class CreateCategoriaDTO
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Descricao { get; set; }
    }
}
