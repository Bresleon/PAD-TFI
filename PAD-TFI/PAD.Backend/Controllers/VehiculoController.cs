using Microsoft.AspNetCore.Mvc;
using PAD.Backend.Dtos;
using PAD.Backend.Services;
using System.ComponentModel.DataAnnotations;

namespace PAD.Backend.Controllers
{
    [ApiController]
    [Route("api/vehiculos")]
    public class VehiculoController : ControllerBase
    {
        private readonly VehiculoService _vehiculoService;
        public VehiculoController(VehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }       

        [HttpGet("obtener-por-pantente")]
        [ProducesResponseType(typeof(VehiculoResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehiculoPorPatente([FromQuery, Required] string patente)
        {
            var vehiculo = await _vehiculoService.ObtenerVehiculoPorPatenteAsync(patente);
            if (vehiculo == null)
            {
                return NotFound($"No se encontró un vehículo con patente {patente}.");
            }
            return Ok(vehiculo);
        }
    }
}
