using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Controllers.Despesas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaVariavelController : ControllerBase
    {
        private readonly IContaVariavelService _service;

        public ContaVariavelController(IContaVariavelService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult IncluirContaVariavel([FromBody] CreateContaVariavelDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirContaVariavel(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterContaVariavel(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterContaVariavel(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        public IActionResult ObterContaVariavels([FromQuery] string? busca)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterContasVariaveis(busca));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarContaVariavel(int id, [FromBody] UpdateContaVariavelDTO DTO)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.EditarContaVariavel(id, DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirContaVariavel(int id)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.ExcluirContaVariavel(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
