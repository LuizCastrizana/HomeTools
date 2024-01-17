using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Controllers.Cartoes
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService _service;

        public FaturaController(IFaturaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult IncluirFatura([FromBody] CreateFaturaDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirOuAtualizarFatura(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpPost("atualizarFatura")]
        public IActionResult AtualizarFatura([FromBody] CreateFaturaDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirOuAtualizarFatura(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterFatura(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterFatura(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet]
        public IActionResult ObterFaturas([FromQuery] string? busca)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterFaturas(busca));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirFatura(int id)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.ExcluirFatura(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }
    }
}
