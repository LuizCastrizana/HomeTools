using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Domain.Interfaces.Repositories.Cartoes
{
    public interface IAssinaturaRepository : IRepository<Assinatura>
    {
        IEnumerable<Assinatura> Obter(Expression<Func<Assinatura, bool>> predicate);
        Task<List<Assinatura>> ObterAsync(Expression<Func<Assinatura, bool>> predicate);
    }
}
