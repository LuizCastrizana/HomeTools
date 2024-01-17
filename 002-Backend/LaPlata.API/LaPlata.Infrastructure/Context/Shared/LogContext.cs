using LaPlata.Domain.Models;

namespace LaPlata.Infrastructure.Context
{
    public class LogContext : BaseContext<Log>
    {
        public LogContext(AppDbContext context) : base(context)
        {
        }
    }
}
