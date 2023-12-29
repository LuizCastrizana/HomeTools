using LaPlata.Domain.BackgroundServices;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using LaPlata.Domain.Services;
using LaPlata.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        hostContext.HostingEnvironment.ApplicationName = "HomeTools.Worker";

        services.AddHostedService<GerarFaturasBGService>();

        services.AddTransient<IFaturaService, FaturaService>();
        services.AddTransient<IContext<Fatura>, FaturaContext>();
    })
    .Build();

await host.RunAsync();