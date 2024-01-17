using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class DespesaContext : BaseContext<Despesa>
    {
        public DespesaContext(AppDbContext context) : base(context)
        {
        }
    }
}
