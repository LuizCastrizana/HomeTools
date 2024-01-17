using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class CategoriaContext : BaseContext<Categoria>
    {
        public CategoriaContext(AppDbContext context) : base(context)
        {
        }
    }
}
