using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces
{
    public interface ICompraService
    {
        RespostaServico<ReadCompraDTO> IncluirCompra(CreateCompraDTO DTO);
        RespostaServico<List<ReadCompraDTO>> ObterCompras(string? busca);
        RespostaServico<List<ReadCompraDTO>> ObterCompras(Expression<Func<Compra, bool>> predicate);
        RespostaServico<ReadCompraDTO> ObterCompra(int id);
        RespostaServico<ReadCompraDTO> EditarCompra(int id, UpdateCompraDTO DTO);
        RespostaServico<ReadCompraDTO> ExcluirCompra(int id);
    }
}
