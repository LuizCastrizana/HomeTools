using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class CartaoContext : BaseContext<Cartao>
    {
        public CartaoContext(AppDbContext context) : base(context)
        {
        }
    }
}
