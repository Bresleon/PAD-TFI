using Microsoft.AspNetCore.Mvc;
using PAD.Backend.Models.Entidades;
using PAD.Backend.Services;

namespace PAD.Backend.Controllers
{
    [Route("api/titulares")]
    [ApiController]
    public class TitularController : ControllerBase
    {
        private readonly TitularService _titularService;
        

        public TitularController(TitularService titularService)
        {
            _titularService = titularService;
        }

        [HttpGet("obtener-por-cuil")]
        [ProducesResponseType(typeof(List<Titular>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTitularPorCuil([FromQuery] string cuil)
        {
            var titular = await _titularService.ObtenerOCrearTitularAsync(cuil);

            if (titular == null)
            {
                return NotFound($"No se encontró un titular con CUIL {cuil}.");
            }

            return Ok(titular);
        }


    }
}
