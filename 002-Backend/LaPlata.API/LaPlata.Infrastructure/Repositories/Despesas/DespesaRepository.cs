using LaPlata.Domain.Interfaces.Repositories.Despesas;
using LaPlata.Domain.Models;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Repositories
{
    public class DespesaRepository : Repository<Despesa>, IDespesaRepository
    {
        public DespesaRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Despesa> Obter(Expression<Func<Despesa, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Categoria);
        }

        public override Task<List<Despesa>> ObterAsync(Expression<Func<Despesa, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Categoria)
                .ToListAsync();
        }
    }
}
