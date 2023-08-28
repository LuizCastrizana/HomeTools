using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface IPagamentoContaService
    {
        RespostaServico<ReadPagamentoContaDTO> IncluirPagamentoConta(CreatePagamentoContaDTO DTO);
        RespostaServico<List<ReadPagamentoContaDTO>> ObterPagamentosConta(string? busca);
        RespostaServico<ReadPagamentoContaDTO> ObterPagamentoConta(int id);
        RespostaServico<ReadPagamentoContaDTO> EditarPagamentoConta(int id, UpdatePagamentoContaDTO DTO);
        RespostaServico<ReadPagamentoContaDTO> ExcluirPagamentoConta(int id);
    }
}
