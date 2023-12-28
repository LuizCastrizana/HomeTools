using LaPlata.Domain.Interfaces;

namespace LaPlata.API.HostedService.Entities
{
    public class FaturaTimer
    {
        private readonly IFaturaService _faturaService;
        private readonly int _intervalo;
        private Timer _timer;

        public FaturaTimer(IFaturaService faturaService)
        {
            _faturaService = faturaService;
            _intervalo = Convert.ToInt32(TimeSpan.FromHours(24).TotalMilliseconds);
        }
        public void IniciarTimer()
        {
            try
            {
                if (_timer != null)
                    _timer.Dispose();

                var tempoEspera = Convert.ToInt32((DateTime.Today.AddDays(1).AddMinutes(5) - DateTime.Now).TotalMilliseconds);
                _timer = new Timer(ExecutarTarefa, null, tempoEspera, _intervalo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao iniciar timer: " + ex.Message);
            }
        }

        public void PararTimer()
        {
            try
            {
                if (_timer != null)
                    _timer.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao parar timer: " + ex.Message);
            }
        }

        private void ExecutarTarefa(object state)
        {
            try
            {
                _faturaService.GerarFaturas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar tarefa: " + ex.Message);
            }
        }
    }
}
