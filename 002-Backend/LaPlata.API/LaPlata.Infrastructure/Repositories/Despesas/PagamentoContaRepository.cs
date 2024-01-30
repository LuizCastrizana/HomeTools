using LaPlata.Domain.Interfaces.Repositories.Despesas;
using LaPlata.Domain.Models;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Repositories
{
    public class PagamentoContaRepository : Repository<PagamentoConta>, IPagamentoContaRepository
    {
        public PagamentoContaRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<PagamentoConta> Obter(Expression<Func<PagamentoConta, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Conta).ThenInclude(x => x.Categoria)
                .Include(x => x.Compra).ThenInclude(x => x.Categoria);
        }

        public override Task<List<PagamentoConta>> ObterAsync(Expression<Func<PagamentoConta, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Conta).ThenInclude(x => x.Categoria)
                .Include(x => x.Compra).ThenInclude(x => x.Categoria)
                .ToListAsync();
        }
    }
}
