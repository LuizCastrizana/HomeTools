using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class AssinaturaContext : BaseContext<Assinatura>
    {
        public AssinaturaContext(AppDbContext context) : base(context)
        {
        }
    }
}
