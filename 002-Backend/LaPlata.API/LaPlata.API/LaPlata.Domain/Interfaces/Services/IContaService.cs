using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface IContaService
    {
        RespostaServico<ReadContaDTO> IncluirConta(CreateContaDTO DTO);
        RespostaServico<List<ReadContaDTO>> ObterContas(string? busca);
        RespostaServico<ReadContaDTO> ObterConta(int id);
        RespostaServico<ReadContaDTO> EditarConta(int id, UpdateContaDTO DTO);
        RespostaServico<ReadContaDTO> ExcluirConta(int id);
    }
}
