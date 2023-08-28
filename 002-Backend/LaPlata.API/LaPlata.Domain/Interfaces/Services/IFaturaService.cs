using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface IFaturaService
    {
        RespostaServico<ReadFaturaDTO> IncluirFatura(CreateFaturaDTO DTO);
        RespostaServico<List<ReadFaturaDTO>> ObterFaturas(string? busca);
        RespostaServico<ReadFaturaDTO> ObterFatura(int id);
        RespostaServico<ReadFaturaDTO> ExcluirFatura(int id);
    }
}
