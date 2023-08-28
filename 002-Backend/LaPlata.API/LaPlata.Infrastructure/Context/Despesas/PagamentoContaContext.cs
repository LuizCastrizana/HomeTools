using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class PagamentoContaContext : IContext<PagamentoConta>
    {
        private readonly AppDbContext _appDbContext;

        public PagamentoContaContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(PagamentoConta obj)
        {
            _appDbContext.PagamentosConta.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<PagamentoConta> Obter(Expression<Func<PagamentoConta, bool>> predicate)
        {
            return _appDbContext.PagamentosConta.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(PagamentoConta obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.PagamentosConta.Attach(obj);
                _appDbContext.PagamentosConta.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
