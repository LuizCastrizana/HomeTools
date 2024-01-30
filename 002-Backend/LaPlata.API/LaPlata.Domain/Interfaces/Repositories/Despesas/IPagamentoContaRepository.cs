using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces.Repositories.Despesas
{
    public interface IPagamentoContaRepository : IRepository<PagamentoConta>
    {
        IEnumerable<PagamentoConta> Obter(Expression<Func<PagamentoConta, bool>> predicate);
        Task<List<PagamentoConta>> ObterAsync(Expression<Func<PagamentoConta, bool>> predicate);
    }
}
