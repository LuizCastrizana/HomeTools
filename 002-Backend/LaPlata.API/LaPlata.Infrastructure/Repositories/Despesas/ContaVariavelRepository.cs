using LaPlata.Domain.Interfaces.Repositories.Despesas;
using LaPlata.Domain.Models;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Repositories
{
    public class ContaVariavelRepository : Repository<ContaVariavel>, IContaVariavelRepository
    {
        public ContaVariavelRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<ContaVariavel> Obter(Expression<Func<ContaVariavel, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Pagamentos)
                .Include(x => x.Categoria);
        }

        public override Task<List<ContaVariavel>> ObterAsync(Expression<Func<ContaVariavel, bool>> predicate)
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
