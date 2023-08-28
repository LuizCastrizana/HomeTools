using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class DespesaContext : IContext<Despesa>
    {
        private readonly AppDbContext _appDbContext;

        public DespesaContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(Despesa obj)
        {
            _appDbContext.Despesas.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Despesa> Obter(Expression<Func<Despesa, bool>> predicate)
        {
            return _appDbContext.Despesas.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(Despesa obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.Despesas.Attach(obj);
                _appDbContext.Despesas.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
