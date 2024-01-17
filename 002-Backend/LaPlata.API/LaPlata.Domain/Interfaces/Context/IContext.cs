using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces
{
    public interface IContext<T>
    {
        /// <summary>
        /// Inclui um objeto no context.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Número de registros afetados.</returns>
        int Adicionar(T obj);
        /// <summary>
        /// Obtem uma lista de objetos de acordo com o predicado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Lista de objetos.</returns>
        IEnumerable<T> Obter(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Exclui um objeto do banco de dados.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Número de registros excluidos.</returns>
        int Excluir(T obj, bool exclusaoFisica = false);
        /// <summary>
        /// Salva as alterações no banco de dados.
        /// </summary>
        /// <returns>Número de registros afetados.</returns>
        int SalvarAlteracoes();
        /// <summary>
        /// Salva as alterações no banco de dados.
        /// </summary>
        /// <returns>Número de registros afetados.</returns>
        Task<int> SalvarAlteracoesAsync();
    }
}
