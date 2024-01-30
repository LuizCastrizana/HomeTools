using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces.Repositories.Cartoes
{
    public interface ICartaoRepository : IRepository<Cartao>
    {
        IEnumerable<Cartao> Obter(Expression<Func<Cartao, bool>> predicate);
        Task<List<Cartao>> ObterAsync(Expression<Func<Cartao, bool>> predicate);
    }
}
