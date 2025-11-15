using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PAD.Backend.Dtos;
using PAD.Backend.DTOs;
using PAD.Backend.Models.Entidades;
using System.ComponentModel.DataAnnotations;

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
    public async Task<IActionResult> GenerarNuevaPatente(TransaccionAltaRequestDto request)
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
    [ProducesResponseType(typeof(List<TransaccionResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTransaccionesPorRango(
        [FromQuery, Required] DateTime desde,
        [FromQuery] DateTime? hasta = null)
    {
        var transacciones = await _transaccionService.ObtenerPorRangoDeFechaAsync(desde, hasta);

        if (transacciones == null || !transacciones.Any())
        {
            return BadRequest("No se encontraron transacciones en el rango especificado.");
        }

        return Ok(transacciones);
    }

    [HttpGet("obtener-por-dni")]
    [ProducesResponseType(typeof(List<TransaccionResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTransaccionesPorDni([FromQuery, Required] string dni)
    {
        var transacciones = await _transaccionService.ObtenerPorDni(dni);

        if (transacciones == null || !transacciones.Any())
        {
            return BadRequest("No se encontraron transacciones para el dni ingresado.");
        }

        return Ok(transacciones);
    }
}