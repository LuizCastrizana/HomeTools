using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface ICartaoService
    {
        RespostaServico<ReadCartaoDTO> IncluirCartao(CreateCartaoDTO DTO);
        RespostaServico<List<ReadCartaoDTO>> ObterCartoes(string? busca);
        RespostaServico<ReadCartaoDTO> ObterCartao(int id);
        RespostaServico<ReadCartaoDTO> EditarCartao(int id, UpdateCartaoDTO DTO);
        RespostaServico<int> ExcluirCartao(int id);
    }
}
