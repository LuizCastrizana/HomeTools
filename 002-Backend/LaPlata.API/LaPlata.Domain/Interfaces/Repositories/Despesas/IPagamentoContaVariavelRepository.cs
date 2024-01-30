using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces.Repositories.Despesas
{
    public interface IPagamentoContaVariavelRepository : IRepository<PagamentoContaVariavel>
    {
        IEnumerable<PagamentoContaVariavel> Obter(Expression<Func<PagamentoContaVariavel, bool>> predicate);
        Task<List<PagamentoContaVariavel>> ObterAsync(Expression<Func<PagamentoContaVariavel, bool>> predicate);
    }
}
