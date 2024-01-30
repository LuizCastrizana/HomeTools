using LaPlata.Domain.Interfaces.Repositories.Cartoes;
using LaPlata.Domain.Models;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Repositories
{
    public class CartaoRepository : Repository<Cartao>, ICartaoRepository
    {
        public CartaoRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Cartao> Obter(Expression<Func<Cartao, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Faturas)
                .Include(x => x.Assinaturas)
                .Include(x => x.Compras).ThenInclude(x => x.Categoria);
        }

        public override async Task<List<Cartao>> ObterAsync(Expression<Func<Cartao, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Faturas)
                .Include(x => x.Assinaturas)
                .Include(x => x.Compras).ThenInclude(x => x.Categoria)
                .ToListAsync();
        }
    }
}
