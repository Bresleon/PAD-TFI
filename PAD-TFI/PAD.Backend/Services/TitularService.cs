using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.Models.Entidades;

namespace PAD.Backend.Services
{
    public class TitularService
    {
        private readonly ApplicationDbContext _context;

        public TitularService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Titular> ObtenerOCrearTitularAsync(PersonaRenaperDto personaRenaper)
        {

             var titularExistente = await _context.Titulares
            .FirstOrDefaultAsync(t => t.Dni == personaRenaper.Dni);

            if (titularExistente != null)
            {
                return titularExistente;
            }
            var nuevoTitular = new Titular
            {
                Nombre = personaRenaper.Nombre,
                Apellido = personaRenaper.Apellido,
                Dni = personaRenaper.Dni,
                Email = personaRenaper.Mail,
                Telefono = personaRenaper.Telefono
            };

            _context.Titulares.Add(nuevoTitular);
            await _context.SaveChangesAsync();

            return nuevoTitular;
        }
}
}
