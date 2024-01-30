using LaPlata.Domain.Interfaces.Repositories.Cartoes;
using LaPlata.Domain.Models;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Repositories
{
    public class AssinaturaRepository : Repository<Assinatura>, IAssinaturaRepository
    {
        public AssinaturaRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Assinatura> Obter(Expression<Func<Assinatura, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Cartao)
                .Include(x => x.AssinaturasFatura).ThenInclude(x => x.Fatura)
                .ToList();
        }

        public override async Task<List<Assinatura>> ObterAsync(Expression<Func<Assinatura, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Cartao)
                .Include(x => x.AssinaturasFatura).ThenInclude(x => x.Fatura)
                .ToListAsync();
        }
    }
}
