using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class ContaContext : IContext<Conta>
    {
        private readonly AppDbContext _appDbContext;

        public ContaContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(Conta obj)
        {
            _appDbContext.Contas.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Conta> Obter(Expression<Func<Conta, bool>> predicate)
        {
            return _appDbContext.Contas.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(Conta obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.Contas.Attach(obj);
                _appDbContext.Contas.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
