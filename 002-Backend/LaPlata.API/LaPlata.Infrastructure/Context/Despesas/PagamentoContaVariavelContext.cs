using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class PagamentoContaVariavelContext : BaseContext<PagamentoContaVariavel>
    {        
        public PagamentoContaVariavelContext(AppDbContext context) : base(context)
        {
        }
    }
}
