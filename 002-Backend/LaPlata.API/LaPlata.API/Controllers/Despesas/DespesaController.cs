using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Controllers.Despesas
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesaService _service;

        public DespesaController(IDespesaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult IncluirDespesa([FromBody] CreateDespesaDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirDespesa(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterDespesa(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterDespesa(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet]
        public IActionResult ObterDespesas([FromQuery] string? busca)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterDespesas(busca));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarDespesa(int id, [FromBody] UpdateDespesaDTO DTO)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.EditarDespesa(id, DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirDespesa(int id)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.ExcluirDespesa(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }
    }
}
