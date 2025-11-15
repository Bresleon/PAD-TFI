using Microsoft.AspNetCore.Mvc;
using PAD.Backend.DTOs;

[Route("api/[controller]")]
[ApiController]
public class TransaccionesController : ControllerBase
{
    private readonly TransaccionService _transaccionService;

    public TransaccionesController(TransaccionService transaccionService)
    {
        _transaccionService = transaccionService;
    }

    [HttpGet("rango")]
    [ProducesResponseType(typeof(List<TransaccionDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransaccionesPorRango(
        [FromQuery] DateTime desde,
        [FromQuery] DateTime? hasta = null)
    {
    
        var transacciones = await _transaccionService.ObtenerPorRangoDeFechaAsync(desde, hasta);

        if (transacciones == null || !transacciones.Any())
        {
            return NotFound("No se encontraron transacciones en el rango especificado.");
        }

        return Ok(transacciones);
    }
}