using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class LogErroContext : IContext<LogErro>
    {
        private readonly AppDbContext _appDbContext;

        public LogErroContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(LogErro obj)
        {
            _appDbContext.LogsErro.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<LogErro> Obter(Expression<Func<LogErro, bool>> predicate)
        {
            return _appDbContext.LogsErro.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(LogErro obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.LogsErro.Attach(obj);
                _appDbContext.LogsErro.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
