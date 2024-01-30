using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces.Repositories.Despesas
{
    public interface IDespesaRepository : IRepository<Despesa>
    {
        IEnumerable<Despesa> Obter(Expression<Func<Despesa, bool>> predicate);
        Task<List<Despesa>> ObterAsync(Expression<Func<Despesa, bool>> predicate);
    }
}
