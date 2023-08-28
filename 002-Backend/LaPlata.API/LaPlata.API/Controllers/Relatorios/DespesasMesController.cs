using LaPlata.API.Extensions;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaPlata.API.Controllers.Despesas
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasMesController : ControllerBase
    {
        private readonly IRelatorioService _service;

        public DespesasMesController(IRelatorioService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult ObterDespesasMes([FromBody] List<ObterDespesasMesDTO> DTO)
        {
            var resposta = new RespostaApi<List<DespesasMesDTO>>();

            try
            {
                return this.TratarRespostaServico(_service.ObterDespesasMes(DTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
