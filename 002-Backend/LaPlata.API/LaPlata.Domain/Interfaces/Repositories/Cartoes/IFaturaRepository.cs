using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces.Repositories.Cartoes
{
    public interface IFaturaRepository : IRepository<Fatura>
    {
        IEnumerable<Fatura> Obter(Expression<Func<Fatura, bool>> predicate);
        Task<List<Fatura>> ObterAsync(Expression<Func<Fatura, bool>> predicate);
    }
}
