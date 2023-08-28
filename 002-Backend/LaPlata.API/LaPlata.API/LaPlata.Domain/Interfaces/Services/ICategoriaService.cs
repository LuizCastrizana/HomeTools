using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Interfaces
{
    public interface ICategoriaService
    {
        RespostaServico<ReadCategoriaDTO> IncluirCategoria(CreateCategoriaDTO DTO);
        RespostaServico<List<ReadCategoriaDTO>> ObterCategorias(string? busca);
        RespostaServico<ReadCategoriaDTO> ObterCategoria(int id);
        RespostaServico<ReadCategoriaDTO> EditarCategoria(int id, string descricao);
        RespostaServico<ReadCategoriaDTO> ExcluirCategoria(int id);
    }
}
