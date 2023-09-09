using LaPlata.Domain.Enums;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LaPlata.API.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult TratarRespostaServico<T>(this ControllerBase controllerBase, RespostaServico<T> respostaServico)
        {
            var respostaApi = new RespostaApi<T>();
            respostaApi.valor = respostaServico.Valor;
            respostaApi.mensagem = respostaServico.Mensagem;
            respostaApi.erros = respostaServico.Erros;
            switch (respostaServico.Status)
            {
                case EnumStatusResposta.SUCESSO:
                    respostaApi.status = StatusCodes.Status200OK;
                    return controllerBase.Ok(JsonConvert.SerializeObject(respostaApi));
                case EnumStatusResposta.VALIDACAO_REJEITADA:
                    respostaApi.status = StatusCodes.Status400BadRequest;
                    return controllerBase.BadRequest(JsonConvert.SerializeObject(respostaApi));
                case EnumStatusResposta.ERRO:
                    respostaApi.status = StatusCodes.Status500InternalServerError;
                    return controllerBase.StatusCode(StatusCodes.Status500InternalServerError, JsonConvert.SerializeObject(respostaApi));
                case EnumStatusResposta.NAO_ENCONTRADO:
                    respostaApi.status = StatusCodes.Status404NotFound;
                    return controllerBase.NotFound(JsonConvert.SerializeObject(respostaApi));
                default:
                    respostaApi.status = StatusCodes.Status204NoContent;
                    return controllerBase.NoContent();
            }
        }
    }
}
