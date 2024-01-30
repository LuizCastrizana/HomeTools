using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace LaPlata.Domain.BackgroundServices
{
    public class GerarFaturasBGService : BackgroundService
    {
        private readonly IFaturaService _faturaService;
        private readonly IRepository<Log> _logRepository;

        public GerarFaturasBGService(IFaturaService faturaService, IRepository<Log> logRepository)
        {
            _faturaService = faturaService;
            _logRepository = logRepository;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellingToken)
        {
            while (!cancellingToken.IsCancellationRequested)
            {
                var intervalo = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);

                try
                {
                    _logRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Iniciando geração de faturas."));

                    var resposta = _faturaService.GerarFaturas();

                    if (resposta != null)
                    {
                        if (resposta.Status == EnumStatusResposta.SUCESSO)
                        {
                            // Se retornou sucesso altera intervalo para executar novamente no dia seguinte a 00:05
                            intervalo = Convert.ToInt32((DateTime.Today.AddDays(1).AddMinutes(5) - DateTime.Now).TotalMilliseconds);

                            _logRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Sucesso! " + resposta.Valor));
                        }
                        else
                        {
                            _logRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Erro: " + resposta.Mensagem));
                        }
                    }

                    await Task.Delay(intervalo, cancellingToken);
                }
                catch (Exception ex)
                {
                    _logRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Erro: " + ex.Message));
                }
            }
        }
    }
}
