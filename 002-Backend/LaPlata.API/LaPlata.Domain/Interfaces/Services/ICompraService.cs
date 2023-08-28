using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface ICompraService
    {
        RespostaServico<ReadCompraDTO> IncluirCompra(CreateCompraDTO DTO);
        RespostaServico<List<ReadCompraDTO>> ObterCompras(string? busca);
        RespostaServico<ReadCompraDTO> ObterCompra(int id);
        RespostaServico<ReadCompraDTO> EditarCompra(int id, UpdateCompraDTO DTO);
        RespostaServico<ReadCompraDTO> ExcluirCompra(int id);
    }
}
