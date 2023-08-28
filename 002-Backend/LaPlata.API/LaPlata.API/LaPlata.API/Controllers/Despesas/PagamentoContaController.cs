using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LaPlata.API.Controllers.Despesas
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoContaController : ControllerBase
    {
        private readonly IPagamentoContaService _service;

        public PagamentoContaController(IPagamentoContaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult IncluirPagamentoConta([FromBody] CreatePagamentoContaDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirPagamentoConta(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPagamentoConta(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterPagamentoConta(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        public IActionResult ObterPagamentoContas([FromQuery] string? busca)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterPagamentosConta(busca));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarPagamentoConta(int id, [FromBody] UpdatePagamentoContaDTO DTO)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.EditarPagamentoConta(id, DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirPagamentoConta(int id)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.ExcluirPagamentoConta(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
