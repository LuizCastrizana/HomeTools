using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class ContaVariavelContext : IContext<ContaVariavel>
    {
        private readonly AppDbContext _appDbContext;

        public ContaVariavelContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(ContaVariavel obj)
        {
            _appDbContext.ContasVariaveis.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<ContaVariavel> Obter(Expression<Func<ContaVariavel, bool>> predicate)
        {
            return _appDbContext.ContasVariaveis.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(ContaVariavel obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.ContasVariaveis.Attach(obj);
                _appDbContext.ContasVariaveis.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
