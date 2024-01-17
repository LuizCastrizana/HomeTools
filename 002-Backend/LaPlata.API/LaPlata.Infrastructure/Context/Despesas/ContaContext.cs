using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class ContaContext : BaseContext<Conta>
    {
        public ContaContext(AppDbContext context) : base(context)
        {
        }
    }
}
