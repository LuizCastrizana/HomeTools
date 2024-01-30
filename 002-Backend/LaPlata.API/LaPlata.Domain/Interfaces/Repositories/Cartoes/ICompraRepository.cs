using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces.Repositories.Cartoes
{
    public interface ICompraRepository : IRepository<Compra>
    {
        IEnumerable<Compra> Obter(Expression<Func<Compra, bool>> predicate);
        Task<List<Compra>> ObterAsync(Expression<Func<Compra, bool>> predicate);
    }
}
