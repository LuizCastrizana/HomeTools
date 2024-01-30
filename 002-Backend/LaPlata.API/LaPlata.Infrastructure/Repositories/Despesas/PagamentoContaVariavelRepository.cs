using LaPlata.Domain.Interfaces.Repositories.Despesas;
using LaPlata.Domain.Models;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Repositories
{
    public class PagamentoContaVariavelRepository : Repository<PagamentoContaVariavel>, IPagamentoContaVariavelRepository
    {
        public PagamentoContaVariavelRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<PagamentoContaVariavel> Obter(Expression<Func<PagamentoContaVariavel, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.ContaVariavel).ThenInclude(x=>x.Categoria)
                .Include(x => x.Compra).ThenInclude(x => x.Categoria);
        }

        public override Task<List<PagamentoContaVariavel>> ObterAsync(Expression<Func<PagamentoContaVariavel, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.ContaVariavel).ThenInclude(x => x.Categoria)
                .Include(x => x.Compra).ThenInclude(x => x.Categoria)
                .ToListAsync();
        }
    }
}
