using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class LogContext : IContext<Log>
    {
        private readonly AppDbContext _appDbContext;

        public LogContext(AppDbContext context)
        {
            _appDbContext = context;
        }

        public int Adicionar(Log obj)
        {
            _appDbContext.Logs.Add(obj);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Log> Obter(Expression<Func<Log, bool>> predicate)
        {
            return _appDbContext.Logs.Where(predicate).Where(x => x.Ativo);
        }

        public int SalvarAlteracoes()
        {
            return _appDbContext.SaveChanges();
        }

        public int Excluir(Log obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _appDbContext.Logs.Attach(obj);
                _appDbContext.Logs.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _appDbContext.SaveChanges();
        }
    }
}
