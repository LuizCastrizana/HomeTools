using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface IFaturaService
    {
        RespostaServico<ReadFaturaDTO> IncluirOuAtualizarFatura(CreateFaturaDTO DTO);
        RespostaServico<List<ReadFaturaDTO>> ObterFaturas(string? busca);
        RespostaServico<ReadFaturaDTO> ObterFatura(int id);
        RespostaServico<ReadFaturaDTO> ExcluirFatura(int id);
        RespostaServico<ReadFaturaDTO> RealizarPagamentoFatura(int id);
        RespostaServico<string> GerarFaturas();
    }
}
