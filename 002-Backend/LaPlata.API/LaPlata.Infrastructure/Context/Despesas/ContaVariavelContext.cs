using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class ContaVariavelContext : BaseContext<ContaVariavel>
    {
        public ContaVariavelContext(AppDbContext context) : base(context)
        {
        }
    }
}
