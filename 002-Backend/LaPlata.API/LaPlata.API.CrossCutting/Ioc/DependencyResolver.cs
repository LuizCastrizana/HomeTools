using AutoMapper;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using LaPlata.Domain.Services;
using LaPlata.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

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
            services.AddTransient<IContext<Cartao>, CartaoContext>();
            services.AddTransient<IContext<Assinatura>, AssinaturaContext>();
            services.AddTransient<IContext<Compra>, CompraContext>();
            services.AddTransient<IContext<Fatura>, FaturaContext>();
            services.AddTransient<IContext<CompraFatura>, CompraFaturaContext>();
            services.AddTransient<IContext<AssinaturaFatura>, AssinaturaFaturaContext>();
            services.AddTransient<IContext<Despesa>, DespesaContext>();
            services.AddTransient<IContext<Conta>, ContaContext>();
            services.AddTransient<IContext<ContaVariavel>, ContaVariavelContext>();
            services.AddTransient<IContext<PagamentoContaVariavel>, PagamentoContaVariavelContext>();
            services.AddTransient<IContext<PagamentoConta>, PagamentoContaContext>();
            services.AddTransient<IContext<Categoria>, CategoriaContext>();
            services.AddTransient<IContext<Log>, LogContext>();

            return services;
        }

        public static IServiceCollection ConfigureDataBase(this IServiceCollection services, IConfigurationRoot configurationBuilder)
        {
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseLazyLoadingProxies().UseMySQL(configurationBuilder.GetConnectionString("LaPlataConnection"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            return services;
        }
    }
}
