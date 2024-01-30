using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Interfaces.Repositories.Cartoes;
using LaPlata.Domain.Interfaces.Repositories.Despesas;
using LaPlata.Domain.Services;
using LaPlata.Infrastructure.Context;
using LaPlata.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LaPlata.API.CrossCutting.Ioc
{
    public static class DependencyResolver
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

        public static IServiceCollection ConfigureRepositoriesInjections(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IFaturaRepository, FaturaRepository>();
            services.AddTransient<ICartaoRepository, CartaoRepository>();
            services.AddTransient<IAssinaturaRepository, AssinaturaRepository>();
            services.AddTransient<ICompraRepository, CompraRepository>();

            services.AddTransient<IDespesaRepository, DespesaRepository>();
            services.AddTransient<IContaRepository, ContaRepository>();
            services.AddTransient<IContaVariavelRepository, ContaVariavelRepository>();
            services.AddTransient<IPagamentoContaVariavelRepository, PagamentoContaVariavelRepository>();
            services.AddTransient<IPagamentoContaRepository, PagamentoContaRepository>();


            return services;
        }

        public static IServiceCollection ConfigureDataBase(this IServiceCollection services, IConfigurationRoot configurationBuilder)
        {
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts
                //.UseLazyLoadingProxies()
                .UseMySQL(configurationBuilder.GetConnectionString("LaPlataConnection"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            return services;
        }
    }
}
