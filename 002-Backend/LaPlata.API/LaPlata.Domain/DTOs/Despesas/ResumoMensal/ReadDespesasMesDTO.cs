namespace LaPlata.Domain.DTOs.ResumoMensal
{
    public class ReadDespesasMesDTO
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public List<ReadPagamentoContaDTO>? Contas { get; set; }
        public List<ReadPagamentoContaVariavelDTO>? ContasVariaveis { get; set; }
        public List<ReadDespesaDTO>? Despesas { get; set; }
    }
}
