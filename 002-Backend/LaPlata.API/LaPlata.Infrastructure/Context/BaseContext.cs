using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaPlata.Infrastructure.Context
{
    public class BaseContext<T> : IContext<T> where T : BaseModel
    {
        protected readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseContext(AppDbContext context)
        {
            _dbContext = context;  
            _dbSet = context.Set<T>();
        }

        public int Adicionar(T obj)
        {
            _dbSet.Add(obj);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> Obter(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(x => x.Ativo);
        }

        public async Task<List<T>> ObterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).Where(x => x.Ativo).ToListAsync();
        }

        public int Excluir(T obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _dbSet.Attach(obj);
                _dbSet.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _dbContext.SaveChanges();
        }

        public async Task<int> ExcluirAsync(T obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _dbSet.Attach(obj);
                _dbSet.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return await _dbContext.SaveChangesAsync();
        }

        public int SalvarAlteracoes()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SalvarAlteracoesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
