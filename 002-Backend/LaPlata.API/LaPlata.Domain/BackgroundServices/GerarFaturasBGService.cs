using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using Microsoft.Extensions.Hosting;

namespace LaPlata.Domain.BackgroundServices
{
    public class GerarFaturasBGService : BackgroundService
    {
        private readonly IFaturaService _service;

        public GerarFaturasBGService(IFaturaService service)
        {
            _service = service;
        }
        protected async override Task ExecuteAsync(CancellationToken cancellingToken)
        {
            var intervalo = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);

            while (!cancellingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("Iniciando geração de faturas.");

                    var resposta = _service.GerarFaturas();

                    if (resposta != null)
                    {
                        if (resposta.Status == EnumStatusResposta.SUCESSO)
                        {
                            // Se retornou sucesso executa novamente no dia seguinte a 00:05
                            intervalo = Convert.ToInt32((DateTime.Today.AddDays(1).AddMinutes(5) - DateTime.Now).TotalMilliseconds);
                            Console.WriteLine("Qtd de faturas geradas: " + resposta.Valor);
                        }
                        else
                        {
                            Console.WriteLine("Erro: " + resposta.Mensagem);
                        }
                    }

                    await Task.Delay(intervalo, cancellingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }
    }
}
