using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class CartaoContext : IContext<Cartao>
    {
        private readonly AppDbContext _appDbContext;

        public CartaoContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(Cartao obj)
        {
            _appDbContext.Cartoes.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Cartao> Obter(Expression<Func<Cartao, bool>> predicate)
        {
            return _appDbContext.Cartoes.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(Cartao obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.Cartoes.Attach(obj);
                _appDbContext.Cartoes.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
