using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class PagamentoContaVariavelContext : IContext<PagamentoContaVariavel>
    {
        private readonly AppDbContext _appDbContext;
        
        public PagamentoContaVariavelContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(PagamentoContaVariavel obj)
        {
            _appDbContext.PagamentosContaVariavel.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<PagamentoContaVariavel> Obter(Expression<Func<PagamentoContaVariavel, bool>> predicate)
        {
            return _appDbContext.PagamentosContaVariavel.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(PagamentoContaVariavel obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.PagamentosContaVariavel.Attach(obj);
                _appDbContext.PagamentosContaVariavel.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
