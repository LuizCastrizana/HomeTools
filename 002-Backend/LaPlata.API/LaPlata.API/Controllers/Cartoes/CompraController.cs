using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Controllers.Cartoes
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _service;

        public CompraController(ICompraService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult IncluirCompra([FromBody] CreateCompraDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirCompra(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterCompra(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterCompra(id));
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
                return this.TratarRespostaServico(_service.ObterCompras(x => x.Cartao.Id == id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet]
        public IActionResult ObterCompras([FromQuery] string? busca)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterCompras(busca));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarCompra(int id, [FromBody] UpdateCompraDTO DTO)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.EditarCompra(id, DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCompra(int id)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.ExcluirCompra(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }
    }
}
