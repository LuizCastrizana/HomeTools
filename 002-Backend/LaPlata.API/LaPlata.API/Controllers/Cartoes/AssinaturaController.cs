using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Controllers.Cartoes
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssinaturaController : ControllerBase
    {
        private readonly IAssinaturaService _service;

        public AssinaturaController(IAssinaturaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult IncluirAssinatura([FromBody] CreateAssinaturaDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirAssinatura(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterAssinatura(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterAssinatura(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet("ObterPorCartaoId/{id}")]
        public IActionResult ObterPorCartaoId(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterAssinaturas(x=>x.Cartao.Id == id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet]
        public IActionResult ObterAssinaturas([FromQuery] string? busca)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterAssinaturas(busca));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarAssinatura(int id, [FromBody] UpdateAssinaturaDTO DTO)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.EditarAssinatura(id, DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirAssinatura(int id)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.ExcluirAssinatura(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }
    }
}
