using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class CompraContext : BaseContext<Compra>
    {
        public CompraContext(AppDbContext context) : base(context)
        {
        }
    }
}
