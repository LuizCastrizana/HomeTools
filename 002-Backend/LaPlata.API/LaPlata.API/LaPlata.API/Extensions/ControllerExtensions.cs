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
            respostaApi.Valor = respostaServico.Valor;
            respostaApi.Mensagem = respostaServico.Mensagem;
            respostaApi.Erros = respostaServico.Erros;
            switch (respostaServico.Status)
            {
                case EnumStatusResposta.SUCESSO:
                    return controllerBase.Ok(JsonConvert.SerializeObject(respostaApi));
                case EnumStatusResposta.VALIDACAO_REJEITADA:
                    return controllerBase.BadRequest(JsonConvert.SerializeObject(respostaApi));
                case EnumStatusResposta.ERRO:
                    return controllerBase.StatusCode(StatusCodes.Status500InternalServerError, JsonConvert.SerializeObject(respostaApi));
                case EnumStatusResposta.NAO_ENCONTRADO:
                    return controllerBase.NotFound(JsonConvert.SerializeObject(respostaApi));
                default:
                    return controllerBase.NoContent();
            }
        }
    }
}
