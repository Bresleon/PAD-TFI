using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.Models.Entidades;
using PAD.Backend.Models.Enums;
using PAD.Backend.Utils;

namespace PAD.Backend.Services
{
    public class PatenteService
    {
        private readonly ApplicationDbContext _context;

        public PatenteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Patente> GenerarYCrearPatenteAsync(Vehiculo vehiculo, int titularId)
        {
            string nuevaPatenteNumero;
            bool existe;

            do
            {
                nuevaPatenteNumero = PatenteGenerator.GenerarPatente().Replace(" ", "");
                existe = await PatenteExisteAsync(nuevaPatenteNumero);

            } while (existe);

            Patente nuevaPatente = new Patente
            {
                NumeroPatente = nuevaPatenteNumero,
                Ejemplar = EjemplarPatente.A,
                Vehiculo = vehiculo,
                TitularId = titularId,
                FechaEmision = DateOnly.FromDateTime(DateTime.Today)
            };

            _context.Patentes.Add(nuevaPatente);
            await _context.SaveChangesAsync();

            return nuevaPatente;
        }

        public async Task<bool> PatenteExisteAsync(string numeroPatente)
        {
            return await _context.Patentes
                .AnyAsync(p => p.NumeroPatente == numeroPatente);
        }

        public async Task<Patente?> ObtenerPatentePorVehiculoIdAsync(int vehiculoId)
        {
            return await _context.Patentes
                .FirstOrDefaultAsync(p => p.VehiculoId == vehiculoId);
        }

        public async Task ActualizarPatenteAsync(Patente patente)
        {
            _context.Patentes.Update(patente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> VehiculoTienePatenteAsignadaAsync(int vehiculoId)
        {
            return await _context.Patentes
                .AnyAsync(p => p.VehiculoId == vehiculoId);
        }
        public async Task<Patente?> ObtenerPatenteYVehiculoPorNumeroAsync(string numeroPatente)
        {
            return await _context.Patentes
                .Include(p => p.Vehiculo) 
                    .ThenInclude(v => v.Modelo)
                        .ThenInclude(m => m.Marca)
                .Include(p => p.Vehiculo)
                    .ThenInclude(v => v.Modelo) 
                .FirstOrDefaultAsync(p => p.NumeroPatente == numeroPatente);
        }
    }
}
