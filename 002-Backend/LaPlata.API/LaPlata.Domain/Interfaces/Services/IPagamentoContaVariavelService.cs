using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface IPagamentoContaVariavelService
    {
        RespostaServico<ReadPagamentoContaVariavelDTO> IncluirPagamentoContaVariavel(CreatePagamentoContaVariavelDTO DTO);
        RespostaServico<List<ReadPagamentoContaVariavelDTO>> ObterPagamentosContaVariavel(string? busca);
        RespostaServico<ReadPagamentoContaVariavelDTO> ObterPagamentoContaVariavel(int id);
        RespostaServico<ReadPagamentoContaVariavelDTO> EditarPagamentoContaVariavel(int id, UpdatePagamentoContaVariavelDTO DTO);
        RespostaServico<ReadPagamentoContaVariavelDTO> ExcluirPagamentoContaVariavel(int id);
    }
}
