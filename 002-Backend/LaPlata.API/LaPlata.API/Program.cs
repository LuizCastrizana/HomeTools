using LaPlata.API.Configuration;
using LaPlata.Domain.BackgroundServices;
using AutoMapper;
using LaPlata.API.CrossCutting.AutoMapper;
using LaPlata.API.CrossCutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var autoMappercfg = new MapperConfiguration(config =>
{
    config.AllowNullDestinationValues = false;
    config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
    config.AddProfile(new AutoMapperConfig());
});
builder.Services.AddSingleton(autoMappercfg.CreateMapper());

builder.Services.AddHostedService<GerarFaturasBGService>();
builder.Services.AddCors();

builder.Services.ConfigureServicesInjections()
                .ConfigureRepositoriesInjections()
                .ConfigureDataBase(builder.Configuration)
                .ConfigureModelStateResponse();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
