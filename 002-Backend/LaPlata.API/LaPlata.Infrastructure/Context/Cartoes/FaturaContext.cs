using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class FaturaContext : BaseContext<Fatura>
    {
        public FaturaContext(AppDbContext context) : base(context)
        {
        }
    }
}
