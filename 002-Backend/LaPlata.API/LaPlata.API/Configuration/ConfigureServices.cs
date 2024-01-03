using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureModelStateResponse(this IServiceCollection services)
        {
            services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = (errorContext) =>
                {
                    var errors = new List<string>();

                    foreach (var key in errorContext.ModelState.Keys)
                    {
                        if (!string.IsNullOrEmpty(key))
                            errors.Add(key + ": " + string.Join(',', errorContext.ModelState[key].Errors.Select(x => x.ErrorMessage)));
                    }

                    var result = new RespostaApi<string>
                    {
                        valor = null,
                        mensagem = null,
                        erros = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });

            return services;
        }
    }
}
