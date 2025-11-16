using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.Models.Entidades;

namespace PAD.Backend.Services
{
    public class TitularService
    {
        private readonly ApplicationDbContext _context;
        private readonly RenaperService _renaperService;

        public TitularService(ApplicationDbContext context, RenaperService renaperService)
        {
            _context = context;
            _renaperService = renaperService;
        }

        public async Task<Titular?> ObtenerOCrearTitularAsync(string Cuil)
        {

             var titularExistente = await _context.Titulares
            .FirstOrDefaultAsync(t => t.Cuil == Cuil);

            if (titularExistente != null)
            {
                return titularExistente;
            }
            var personaRenaper = await _renaperService.ObtenerPersonaPorCuilAsync(Cuil);
            if (personaRenaper == null)
            {
                return null;
            }
            var nuevoTitular = new Titular
            {
                Nombre = personaRenaper.Nombre,
                Apellido = personaRenaper.Apellido,
                Dni = personaRenaper.Dni,
                Cuil = Cuil,
                Email = personaRenaper.Mail,
                Telefono = personaRenaper.Telefono
            };

            _context.Titulares.Add(nuevoTitular);
            await _context.SaveChangesAsync();

            return nuevoTitular;
        }
}
}
