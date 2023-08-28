using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface IAssinaturaService
    {
        RespostaServico<ReadAssinaturaDTO> IncluirAssinatura(CreateAssinaturaDTO DTO);
        RespostaServico<List<ReadAssinaturaDTO>> ObterAssinaturas(string? busca);
        RespostaServico<ReadAssinaturaDTO> ObterAssinatura(int id);
        RespostaServico<ReadAssinaturaDTO> EditarAssinatura(int id, UpdateAssinaturaDTO DTO);
        RespostaServico<ReadAssinaturaDTO> ExcluirAssinatura(int id);
    }
}
