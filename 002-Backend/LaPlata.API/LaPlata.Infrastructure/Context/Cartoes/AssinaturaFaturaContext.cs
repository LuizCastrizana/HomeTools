using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class AssinaturaFaturaContext : BaseContext<AssinaturaFatura>
    {
        public AssinaturaFaturaContext(AppDbContext context) : base(context)
        {
        }
    }
}
