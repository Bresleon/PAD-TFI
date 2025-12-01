using Microsoft.AspNetCore.Mvc;
using PAD.Backend.Dtos;
using PAD.Backend.Services;
using System.ComponentModel.DataAnnotations;

namespace PAD.Backend.Controllers
{
    [ApiController]
    [Route("api/titulares/{cuil_titular}/vehiculos")]
    public class TitularVehiculoController : ControllerBase 
    {
        private readonly VehiculoService _vehiculoService;
        public TitularVehiculoController(VehiculoService _vehiculoService)
        {
            this._vehiculoService = _vehiculoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<VehiculoResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehiculosPorTitular(string cuil_titular)
        {
            var vehiculos = await _vehiculoService.ObtenerVehiculosPorCuilAsync(cuil_titular);
            if (vehiculos == null)
            {
                return NotFound($"No se encontró ningún vehículo asociado al cuil: {cuil_titular}.");
            }
            return Ok(vehiculos);
        }
    }
}
