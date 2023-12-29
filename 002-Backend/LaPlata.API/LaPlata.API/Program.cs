using LaPlata.API;
using LaPlata.API.HostedService;
using LaPlata.Domain.Configuration;
using LaPlata.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddHostedService<GerarFaturasHostedService>();
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
