using LaPlata.API.HostedService.Entities;

namespace LaPlata.API.HostedService
{
    public class GerarFaturasHostedService : IHostedService
    {
        private readonly FaturaTimer _timer;

        public GerarFaturasHostedService(FaturaTimer faturaTimer)
        {
            _timer = faturaTimer;
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
