using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Controllers.Despesas
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoContaVariavelController : ControllerBase
    {
        private readonly IPagamentoContaVariavelService _service;

        public PagamentoContaVariavelController(IPagamentoContaVariavelService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult IncluirPagamentoContaVariavel([FromBody] CreatePagamentoContaVariavelDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirPagamentoContaVariavel(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPagamentoContaVariavel(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterPagamentoContaVariavel(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet]
        public IActionResult ObterPagamentoContaVariavels([FromQuery] string? busca)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterPagamentosContaVariavel(busca));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarPagamentoContaVariavel(int id, [FromBody] UpdatePagamentoContaVariavelDTO DTO)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.EditarPagamentoContaVariavel(id, DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirPagamentoContaVariavel(int id)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.ExcluirPagamentoContaVariavel(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }
    }
}
