using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class CompraContext : IContext<Compra>
    {
        private readonly AppDbContext _appDbContext;

        public CompraContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(Compra obj)
        {
            _appDbContext.Compras.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Compra> Obter(Expression<Func<Compra, bool>> predicate)
        {
            return _appDbContext.Compras.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(Compra obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.Compras.Attach(obj);
                _appDbContext.Compras.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
