using LaPlata.Domain.Interfaces.Repositories.Cartoes;
using LaPlata.Domain.Models;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Repositories
{
    public class FaturaRepository : Repository<Fatura>, IFaturaRepository
    {
        public FaturaRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Fatura> Obter(Expression<Func<Fatura, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Cartao)
                .Include(x => x.AssinaturasFatura).ThenInclude(x => x.Assinatura)
                .Include(x => x.ComprasFatura).ThenInclude(x => x.Compra).ThenInclude(x => x.Categoria);
        }

        public override async Task<List<Fatura>> ObterAsync(Expression<Func<Fatura, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Cartao)
                .Include(x => x.AssinaturasFatura).ThenInclude(x => x.Assinatura)
                .Include(x => x.ComprasFatura).ThenInclude(x => x.Compra).ThenInclude(x => x.Categoria)
                .ToListAsync();
        }
    }
}
