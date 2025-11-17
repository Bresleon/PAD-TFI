using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.Dtos;

namespace PAD.Backend.Services;

public class MarcaService
{
    private readonly ApplicationDbContext _context;

    public MarcaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MarcaDto>> ObtenerTodos()
    {
        var marcas = _context.Marcas
            .Include(m => m.Modelos);

        var marcasConModelosDto = await marcas.Select(m => new MarcaDto { Nombre = m.Nombre }).ToListAsync();

        return marcasConModelosDto;
    }
}
