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

        public async Task<Patente> GenerarYCrearPatenteAsync(int vehiculoId, int titularId)
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
                VehiculoId = vehiculoId,
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
    }
}
