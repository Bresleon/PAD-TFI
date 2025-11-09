using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.DTOs;
using PAD.Backend.Models.Entidades;

public class TransaccionService
{
    private readonly ApplicationDbContext _context;

    public TransaccionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TransaccionDTO>> ObtenerPorRangoDeFechaAsync(DateTime desde, DateTime? hasta)
    {
        DateTime fechaHasta = hasta.HasValue ? hasta.Value.Date.AddDays(1).AddSeconds(-1) : DateTime.Now;
        DateTime fechaDesde = desde.Date;
        
        var transaccionesQuery = _context.Transacciones
  
            .Where(t => t.FechaTransaccion >= fechaDesde && t.FechaTransaccion <= fechaHasta)
            
            
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
         
            FechaTransaccion = t.FechaTransaccion,
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
}