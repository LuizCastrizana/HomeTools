using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LaPlata.Domain.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureServicesInjections(this IServiceCollection services)
        {
            services.AddTransient<ICartaoService, CartaoService>();
            services.AddTransient<IAssinaturaService, AssinaturaService>();
            services.AddTransient<ICompraService, CompraService>();
            services.AddTransient<IFaturaService, FaturaService>();
            services.AddTransient<IDespesaService, DespesaService>();
            services.AddTransient<IContaService, ContaService>();
            services.AddTransient<IContaVariavelService, ContaVariavelService>();
            services.AddTransient<IPagamentoContaVariavelService, PagamentoContaVariavelService>();
            services.AddTransient<IPagamentoContaService, PagamentoContaService>();
            services.AddTransient<IRelatorioService, RelatorioService>();
            services.AddTransient<ICategoriaService, CategoriaService>();

            return services;
        }
    }
}
