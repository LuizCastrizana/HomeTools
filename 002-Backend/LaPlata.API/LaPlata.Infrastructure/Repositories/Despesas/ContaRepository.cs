using LaPlata.Domain.Interfaces.Repositories.Despesas;
using LaPlata.Domain.Models;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Conta> Obter(Expression<Func<Conta, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Pagamentos)
                .Include(x => x.Categoria);
        }

        public override Task<List<Conta>> ObterAsync(Expression<Func<Conta, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Pagamentos)
                .Include(x => x.Categoria)
                .ToListAsync();
        }
    }
}
