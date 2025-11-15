using Microsoft.AspNetCore.Mvc;
using PAD.Backend.Dtos;
using PAD.Backend.DTOs;
using PAD.Backend.Models.Entidades;

[Route("api/transacciones")]
[ApiController]
public class TransaccionesController : ControllerBase
{
    private readonly TransaccionService _transaccionService;

    public TransaccionesController(TransaccionService transaccionService)
    {
        _transaccionService = transaccionService;
    }

    [HttpPost("generar-nueva-patente")]
    [ProducesResponseType(typeof(TransaccionDTO), StatusCodes.Status201Created)]  
    public async Task<IActionResult> generarNuevaPatente(TransaccionAltaRequestDto request)
    {
        var transaccion = await _transaccionService.GenerarNuevaPatenteAsync(request);
        return Ok(transaccion);
    }
    [HttpPost("transferir-patente")]
    [ProducesResponseType(typeof(TransaccionDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> TransferirPatente(TransaccionTransferenciaRequestDto request)
    {
        var transaccion = await _transaccionService.TransferirPatenteAsync(request);
        return Ok(transaccion);
    }



    [HttpGet("obtener-por-rango")]
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