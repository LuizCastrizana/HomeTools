using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces.Repositories.Despesas
{
    public interface IContaVariavelRepository : IRepository<ContaVariavel>
    {
        IEnumerable<ContaVariavel> Obter(Expression<Func<ContaVariavel, bool>> predicate);
        Task<List<ContaVariavel>> ObterAsync(Expression<Func<ContaVariavel, bool>> predicate);
    }
}
