using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Controllers.Despesas
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult IncluirCategoria([FromBody] CreateCategoriaDTO DTO)
        {
            try
            {
                return this.TratarRespostaServico(_service.IncluirCategoria(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterCategoria(int id)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterCategoria(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpGet]
        public IActionResult ObterCategorias([FromQuery] string? busca)
        {
            try
            {
                return this.TratarRespostaServico(_service.ObterCategorias(busca));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] string descricao)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.EditarCategoria(id, descricao));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCategoria(int id)
        {
            var resposta = new RespostaApi<int>();

            try
            {
                return this.TratarRespostaServico(_service.ExcluirCategoria(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespostaApi<string>(e.Message));
            }
        }
    }
}
