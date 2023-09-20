using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces
{
    public interface IAssinaturaService
    {
        RespostaServico<ReadAssinaturaDTO> IncluirAssinatura(CreateAssinaturaDTO DTO);
        RespostaServico<List<ReadAssinaturaDTO>> ObterAssinaturas(string? busca);
        RespostaServico<List<ReadAssinaturaDTO>> ObterAssinaturas(Expression<Func<Assinatura, bool>> predicate);
        RespostaServico<ReadAssinaturaDTO> ObterAssinatura(int id);
        RespostaServico<ReadAssinaturaDTO> EditarAssinatura(int id, UpdateAssinaturaDTO DTO);
        RespostaServico<ReadAssinaturaDTO> ExcluirAssinatura(int id);
    }
}
