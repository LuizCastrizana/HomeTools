using LaPlata.Domain.Interfaces.Repositories.Cartoes;
using LaPlata.Domain.Models;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Repositories
{
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        public CompraRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Compra> Obter(Expression<Func<Compra, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Cartao)
                .Include(x => x.Categoria)
                .Include(x => x.ComprasFatura).ThenInclude(x => x.Fatura);
        }

        public override async Task<List<Compra>> ObterAsync(Expression<Func<Compra, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Cartao)
                .Include(x => x.Categoria)
                .Include(x => x.ComprasFatura).ThenInclude(x => x.Fatura)
                .ToListAsync();
        }
    }
}
