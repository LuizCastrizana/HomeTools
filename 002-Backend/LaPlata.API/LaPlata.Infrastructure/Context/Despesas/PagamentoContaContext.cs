using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class PagamentoContaContext : BaseContext<PagamentoConta>
    {
        public PagamentoContaContext(AppDbContext context) : base(context)
        {
        }
    }
}
