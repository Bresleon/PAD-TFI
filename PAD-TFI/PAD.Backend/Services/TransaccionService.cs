using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PAD.Backend.Data;
using PAD.Backend.Dtos;
using PAD.Backend.DTOs;
using PAD.Backend.Models.Entidades;
using PAD.Backend.Models.Enums;
using PAD.Backend.Services;
using PAD.Backend.ThirdPartyServiceCommunication.MercadoPago.Service;
using PAD.Backend.Utils;
using System.Net;

public class TransaccionService
{
    private readonly ApplicationDbContext _context;
    private readonly TitularService _titularService;
    private readonly PatenteService _patenteService;
    private readonly VehiculoService _vehiculoService;
    private readonly MercadoPagoService _mercadoPagoService;

    public TransaccionService(ApplicationDbContext context,  TitularService titularService, PatenteService patenteService, VehiculoService vehiculoService, MercadoPagoService mercadoPagoService)
    {
        _context = context;
        _titularService = titularService;
        _patenteService = patenteService;
        _vehiculoService = vehiculoService;
        _mercadoPagoService = mercadoPagoService;
    }

    public async Task<List<TransaccionResponseDTO>> ObtenerPorDni(string dni)
    {
        var transaccionesQuery = _context.Transacciones
            .Where(t => t.TitularDestino.Dni == dni)
            .Include(t => t.TitularDestino)
            .Include(t => t.TitularOrigen)
            .Include(t => t.Patente)
                .ThenInclude(p => p.Vehiculo)
                    .ThenInclude(v => v.Modelo)
                    .ThenInclude(m => m.Marca)
            .Include(t => t.Patente)
                .ThenInclude(p => p.Vehiculo)
                    .ThenInclude(v => v.Modelo);

        var resultadoDTO = await transaccionesQuery.Select(t => new TransaccionResponseDTO
        {
            FechaTransaccion = t.Fecha.ToDateTime(TimeOnly.MinValue),
            CostoOperacion = t.Costo,
            TipoTransaccion = t.TipoTransaccion.ToString(),

            TitularOrigen = t.TitularOrigen != null ? $"{t.TitularOrigen.Nombre} {t.TitularOrigen.Apellido}" : "N/A",
            TitularDestino = $"{t.TitularDestino.Nombre} {t.TitularDestino.Apellido}",

            NumeroPatente = t.Patente.NumeroPatente,
            EjemplarPatente = t.Patente.Ejemplar.ToString(),

            Marca = t.Patente.Vehiculo.Modelo.Marca.Nombre,
            Modelo = t.Patente.Vehiculo.Modelo.Nombre,
            AnioFabricacion = t.Patente.Vehiculo.FechaFabricacion.Year,
            NumeroMotor = t.Patente.Vehiculo.NumeroMotor,
            CategoriaVehiculo = t.Patente.Vehiculo.Categoria.ToString()

        }).ToListAsync();

        return resultadoDTO;
    }

    public async Task<List<TransaccionResponseDTO>> ObtenerPorRangoDeFechaAsync(DateTime desde, DateTime? hasta)
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
                    .ThenInclude(v => v.Modelo)
                    .ThenInclude(m => m.Marca)
            .Include(t => t.Patente)
                .ThenInclude(p => p.Vehiculo)
                    .ThenInclude(v => v.Modelo);

       
        var resultadoDTO = await transaccionesQuery.Select(t => new TransaccionResponseDTO
        {
         
            FechaTransaccion = t.Fecha.ToDateTime(TimeOnly.MinValue),
            CostoOperacion = t.Costo,
            TipoTransaccion = t.TipoTransaccion.ToString(), 

            TitularOrigen = t.TitularOrigen != null ? $"{t.TitularOrigen.Nombre} {t.TitularOrigen.Apellido}" : "N/A",
            TitularDestino = $"{t.TitularDestino.Nombre} {t.TitularDestino.Apellido}", 
         
            NumeroPatente = t.Patente.NumeroPatente,
            EjemplarPatente = t.Patente.Ejemplar.ToString(),

            Marca = t.Patente.Vehiculo.Modelo.Marca.Nombre,
            Modelo = t.Patente.Vehiculo.Modelo.Nombre,
            AnioFabricacion = t.Patente.Vehiculo.FechaFabricacion.Year, 
            NumeroMotor = t.Patente.Vehiculo.NumeroMotor,
            CategoriaVehiculo = t.Patente.Vehiculo.Categoria.ToString() 

        }).ToListAsync();

        return resultadoDTO;
    }

    public async Task<TransaccionDTO> GenerarNuevaPatenteAsync(TransaccionAltaRequestDto request)
    {
        await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var titular = await _titularService.ObtenerOCrearTitularAsync(request.Titular);

            int marcaId = await _vehiculoService.ObtenerMarcaIdPorNombreAsync(request.Marca);
            int modeloId = await _vehiculoService.ObtenerModeloIdPorNombreAsync(request.Modelo);
            await _vehiculoService.ValidarUnicidadVehiculoAsync(
                request.NumeroChasis,
                request.NumeroMotor
            );
            var vehiculo = _vehiculoService.CrearVehiculo(
                marcaId, modeloId, request.Categoria,
                request.Precio, request.FechaFabricacion,
                request.NumeroChasis, request.NumeroMotor
            );

            var patente = await _patenteService.GenerarYCrearPatenteAsync(vehiculo, titular.Id);

            const decimal PorcentajeCosto = 0.05m; 
                                               
            decimal costoOperacion = vehiculo.Precio * PorcentajeCosto;

            var preferencia = await _mercadoPagoService.CrearPreferenciaPagoAltaPatenteAsync(patente.NumeroPatente);

            var nuevaTransaccion = new Transaccion
            {
                Fecha = DateOnly.FromDateTime(DateTime.Today),
                Costo = costoOperacion,
                TipoTransaccion = TipoTransaccion.ALTA,
                TitularDestinoId = titular.Id,
                PatenteId = patente.Id,
                ExternalReference = preferencia.Id,
            };

            await _context.Transacciones.AddAsync(nuevaTransaccion);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var resultadoDTO = new TransaccionDTO
            {
                LinkDePagoMP = preferencia.InitPoint,
                FechaTransaccion = nuevaTransaccion.Fecha.ToDateTime(TimeOnly.MinValue),
                CostoOperacion = nuevaTransaccion.Costo,
                TipoTransaccion = nuevaTransaccion.TipoTransaccion.ToString(),

                TitularOrigen = "N/A (Alta)",
                TitularDestino = $"{titular.Nombre} {titular.Apellido}",

                NumeroPatente = patente.NumeroPatente,
                EjemplarPatente = patente.Ejemplar.ToString(),

                Marca = request.Marca,
                Modelo = request.Modelo,
                AnioFabricacion = request.FechaFabricacion.Year,
                NumeroMotor = vehiculo.NumeroMotor,
                CategoriaVehiculo = vehiculo.Categoria.ToString()
            };
            return resultadoDTO;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw; 
        }
    }

    public async Task<TransaccionDTO> TransferirPatenteAsync(TransaccionTransferenciaRequestDto request)
    {
        await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var patente = await _patenteService.ObtenerPatenteYVehiculoPorNumeroAsync(request.NumeroPatente);

            if (patente == null)
            {
                throw new InvalidOperationException($"La patente '{request.NumeroPatente}' no fue encontrada.");
            }

            var vehiculo = patente.Vehiculo;

            if (vehiculo == null)
            {
                throw new InvalidOperationException($"El vehículo asociado a la patente '{request.NumeroPatente}' no fue encontrado.");
            }

            var titularOrigen = await _titularService.ObtenerOCrearTitularAsync(request.TitularOrigen);
            var titularDestino = await _titularService.ObtenerOCrearTitularAsync(request.TitularDestino);


            if (patente == null || patente.TitularId != titularOrigen.Id)
                throw new Exception($"Patente no encontrada para el vehículo o el titular de origen es incorrecto.");

            patente.TitularId = titularDestino.Id;
            _context.Patentes.Update(patente);
    
            const decimal PorcentajeCosto = 0.015m;
            decimal costoOperacionReal = vehiculo.Precio * PorcentajeCosto;
            decimal montoCobro = 1.00m;

            var preferencia = await _mercadoPagoService.CrearPreferenciaPagoTransferenciPatenteAsync(
                 $"Transferencia de Patente - {patente.NumeroPatente}",
                 montoCobro
            );

            var nuevaTransaccion = new Transaccion
            {
                Fecha = DateOnly.FromDateTime(DateTime.Today),
                Costo = costoOperacionReal,
                TipoTransaccion = TipoTransaccion.TRANSFERENCIA,
                TitularOrigenId = titularOrigen.Id,
                TitularDestinoId = titularDestino.Id,
                PatenteId = patente.Id,
                ExternalReference = preferencia.Id,
            };

            await _context.Transacciones.AddAsync(nuevaTransaccion);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new TransaccionDTO
            {
                LinkDePagoMP = preferencia.InitPoint,
                FechaTransaccion = nuevaTransaccion.Fecha.ToDateTime(TimeOnly.MinValue),
                CostoOperacion = nuevaTransaccion.Costo,
                TipoTransaccion = nuevaTransaccion.TipoTransaccion.ToString(),

                TitularOrigen = $"{titularOrigen.Nombre} {titularOrigen.Apellido}",
                TitularDestino = $"{titularDestino.Nombre} {titularDestino.Apellido}",

                NumeroPatente = patente.NumeroPatente,
                EjemplarPatente = patente.Ejemplar.ToString(),

                Marca = vehiculo.Modelo.Marca.Nombre,
                Modelo = vehiculo.Modelo.Nombre,
                AnioFabricacion = vehiculo.FechaFabricacion.Year,
                NumeroMotor = vehiculo.NumeroMotor,
                CategoriaVehiculo = vehiculo.Categoria.ToString(),
            };
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}