using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface IDespesaService
    {
        RespostaServico<ReadDespesaDTO> IncluirDespesa(CreateDespesaDTO DTO);
        RespostaServico<List<ReadDespesaDTO>> ObterDespesas(string? busca);
        RespostaServico<ReadDespesaDTO> ObterDespesa(int id);
        RespostaServico<ReadDespesaDTO> EditarDespesa(int id, UpdateDespesaDTO DTO);
        RespostaServico<ReadDespesaDTO> ExcluirDespesa(int id);
    }
}
