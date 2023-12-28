using LaPlata.API.HostedService.Entities;
using LaPlata.Domain.Interfaces;

namespace LaPlata.API.HostedService
{
    public class GerarFaturasHostedService : IHostedService
    {
        private readonly FaturaTimer _timer;

        public GerarFaturasHostedService(IFaturaService service)
        {
            _timer = new FaturaTimer(service);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer.IniciarTimer();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.PararTimer();
            return Task.CompletedTask;
        }
    }
}
