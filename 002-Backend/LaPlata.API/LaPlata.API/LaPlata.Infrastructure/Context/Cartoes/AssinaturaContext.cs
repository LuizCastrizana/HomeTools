using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class AssinaturaContext : IContext<Assinatura>
    {
        private readonly AppDbContext _appDbContext;

        public AssinaturaContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(Assinatura obj)
        {
            _appDbContext.Assinaturas.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Assinatura> Obter(Expression<Func<Assinatura, bool>> predicate)
        {
            return _appDbContext.Assinaturas.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(Assinatura obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.Assinaturas.Attach(obj);
                _appDbContext.Assinaturas.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
