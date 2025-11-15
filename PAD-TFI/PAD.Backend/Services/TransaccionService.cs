using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.Dtos;
using PAD.Backend.DTOs;
using PAD.Backend.Models.Entidades;
using PAD.Backend.Models.Enums;
using PAD.Backend.Services;
using PAD.Backend.Utils;
using System.Net;

public class TransaccionService
{
    private readonly ApplicationDbContext _context;
    private readonly RenaperService _renaperService;
    private readonly TitularService _titularService;
    private readonly PatenteService _patenteService;

    public TransaccionService(ApplicationDbContext context, RenaperService renaperService, TitularService titularService, PatenteService patenteService)
    {
        _context = context;
        _renaperService = renaperService;
        _titularService = titularService;
        _patenteService = patenteService;
    }

    public async Task<List<TransaccionDTO>> ObtenerPorRangoDeFechaAsync(DateTime desde, DateTime? hasta)
    {
        DateOnly fechaDesde = DateOnly.FromDateTime(desde);

        DateOnly fechaHasta = hasta.HasValue
            ? DateOnly.FromDateTime(hasta.Value)
            : DateOnly.FromDateTime(DateTime.Today);

        var transaccionesQuery = _context.Transacciones
  
            .Where(t => t.Fecha >= fechaDesde && t.Fecha <= fechaHasta)
            
            
            .Include(t => t.TitularDestino)
            .Include(t => t.TitularOrigen) 
            .Include(t => t.Patente) 
                .ThenInclude(p => p.Vehiculo)
                    .ThenInclude(v => v.Marca)
            .Include(t => t.Patente)
                .ThenInclude(p => p.Vehiculo)
                    .ThenInclude(v => v.Modelo);

       
        var resultadoDTO = await transaccionesQuery.Select(t => new TransaccionDTO
        {
         
            FechaTransaccion = t.Fecha.ToDateTime(TimeOnly.MinValue),
            CostoOperacion = t.Costo,
            TipoTransaccion = t.TipoTransaccion.ToString(), 

            TitularOrigen = t.TitularOrigen != null ? $"{t.TitularOrigen.Nombre} {t.TitularOrigen.Apellido}" : "N/A",
            TitularDestino = $"{t.TitularDestino.Nombre} {t.TitularDestino.Apellido}", 
         
            NumeroPatente = t.Patente.NumeroPatente,
            EjemplarPatente = t.Patente.Ejemplar.ToString(), 
            
            Marca = t.Patente.Vehiculo.Marca.Nombre,
            Modelo = t.Patente.Vehiculo.Modelo.Nombre,
            AnioFabricacion = t.Patente.Vehiculo.FechaFabricacion.Year, 
            NumeroMotor = t.Patente.Vehiculo.NumeroMotor,
            CategoriaVehiculo = t.Patente.Vehiculo.Categoria.ToString() 

        }).ToListAsync();

        return resultadoDTO;
    }

    public async Task<TransaccionDTO> GenerarNuevaPatenteAsync(TransaccionAltaRequestDto request)
    {
        PersonaRenaperDto? personaRenaper = await _renaperService.ObtenerPersonaPorCuilAsync(request.Titular);
        if (personaRenaper == null)
        {
            throw new Exception("No se pudo obtener la información de la persona desde RENAPER.");
        }

        var titular = await _titularService.ObtenerOCrearTitularAsync(personaRenaper);    

        var patente = await _patenteService.GenerarYCrearPatenteAsync(request.VehiculoId, titular.Id);

        throw new NotImplementedException();
    }
}