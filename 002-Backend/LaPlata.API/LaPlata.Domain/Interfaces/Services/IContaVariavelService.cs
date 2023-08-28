using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface IContaVariavelService
    {
        RespostaServico<ReadContaVariavelDTO> IncluirContaVariavel(CreateContaVariavelDTO DTO);
        RespostaServico<List<ReadContaVariavelDTO>> ObterContasVariaveis(string? busca);
        RespostaServico<ReadContaVariavelDTO> ObterContaVariavel(int id);
        RespostaServico<ReadContaVariavelDTO> EditarContaVariavel(int id, UpdateContaVariavelDTO DTO);
        RespostaServico<ReadContaVariavelDTO> ExcluirContaVariavel(int id);
    }
}
