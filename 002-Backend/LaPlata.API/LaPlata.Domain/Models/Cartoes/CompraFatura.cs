﻿using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.Models
{
    public class CompraFatura : AuditableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public virtual Fatura Fatura { get; set; }
        [Required]
        public int FaturaId { get; set; }
        public virtual Compra Compra { get; set; }
        [Required]
        public int CompraId { get; set; }
        [Required]
        public int Parcela { get; set; }
        [Required]
        public Decimal ValorParcela { get; set; }
    }
}
