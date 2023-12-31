﻿using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.DTOs
{
    public class UpdateCompraDTO
    {
        [MinLength(1, ErrorMessage = "Valor inválido")]
        [MaxLength(50, ErrorMessage = "Valor inválido")]
        public string? Descricao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Valor inválido")]
        public int? ValorInteiro { get; set; }

        [Range(0, 99, ErrorMessage = "Valor inválido")]
        public int? ValorCentavos { get; set; }

        public DateTime? DataCompra { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Valor inválido")]
        public int? QtdParcelas { get; set; }

        public bool? Paga { get; set; }

        public int? CartaoId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Valor inválido")]
        public int? CategoriaId { get; set; }
    }
}
