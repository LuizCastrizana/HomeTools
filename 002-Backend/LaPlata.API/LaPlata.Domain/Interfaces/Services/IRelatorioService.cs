using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface IRelatorioService
    {
        RespostaServico<List<DespesasMesDTO>> ObterDespesasMes(List<ObterDespesasMesDTO> DTO);
    }
}
