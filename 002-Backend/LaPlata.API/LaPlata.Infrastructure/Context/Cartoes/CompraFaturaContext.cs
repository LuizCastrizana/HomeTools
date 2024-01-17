using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class CompraFaturaContext : BaseContext<CompraFatura>
    {
        public CompraFaturaContext(AppDbContext context) : base(context)
        {
        }
    }
}
