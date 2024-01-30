using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces.Repositories.Despesas
{
    public interface IContaRepository : IRepository<Conta>
    {
        IEnumerable<Conta> Obter(Expression<Func<Conta, bool>> predicate);
        Task<List<Conta>> ObterAsync(Expression<Func<Conta, bool>> predicate);
    }
}
