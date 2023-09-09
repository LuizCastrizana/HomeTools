using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Controllers.Despesas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _service;

        public ContaController(IContaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult IncluirConta([FromBody] CreateContaDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirConta(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterConta(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterConta(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet]
        public IActionResult ObterContas([FromQuery] string? busca)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterContas(busca));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarConta(int id, [FromBody] UpdateContaDTO DTO)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.EditarConta(id, DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirConta(int id)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.ExcluirConta(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }
    }
}
