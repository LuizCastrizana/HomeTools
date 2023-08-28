using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class CompraFaturaContext : IContext<CompraFatura>
    {
        private readonly AppDbContext _appDbContext;

        public CompraFaturaContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(CompraFatura obj)
        {
            _appDbContext.ComprasFatura.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<CompraFatura> Obter(Expression<Func<CompraFatura, bool>> predicate)
        {
            return _appDbContext.ComprasFatura.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(CompraFatura obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.ComprasFatura.Attach(obj);
                _appDbContext.ComprasFatura.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
