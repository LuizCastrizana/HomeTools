using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class AssinaturaFaturaContext : IContext<AssinaturaFatura>
    {
        private readonly AppDbContext _appDbContext;

        public AssinaturaFaturaContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(AssinaturaFatura obj)
        {
            _appDbContext.AssinaturasFatura.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<AssinaturaFatura> Obter(Expression<Func<AssinaturaFatura, bool>> predicate)
        {
            return _appDbContext.AssinaturasFatura.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(AssinaturaFatura obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.AssinaturasFatura.Attach(obj);
                _appDbContext.AssinaturasFatura.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
