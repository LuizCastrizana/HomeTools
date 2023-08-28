using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class FaturaContext : IContext<Fatura>
    {
        private readonly AppDbContext _appDbContext;

        public FaturaContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(Fatura obj)
        {
            _appDbContext.Faturas.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Fatura> Obter(Expression<Func<Fatura, bool>> predicate)
        {
            return _appDbContext.Faturas.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(Fatura obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.Faturas.Attach(obj);
                _appDbContext.Faturas.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
