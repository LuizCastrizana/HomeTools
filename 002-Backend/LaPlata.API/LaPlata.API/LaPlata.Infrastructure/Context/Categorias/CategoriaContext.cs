using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class CategoriaContext : IContext<Categoria>
    {
        private readonly AppDbContext _appDbContext;

        public CategoriaContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(Categoria obj)
        {
            _appDbContext.Categorias.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Categoria> Obter(Expression<Func<Categoria, bool>> predicate)
        {
            return _appDbContext.Categorias.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(Categoria obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.Categorias.Attach(obj);
                _appDbContext.Categorias.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
