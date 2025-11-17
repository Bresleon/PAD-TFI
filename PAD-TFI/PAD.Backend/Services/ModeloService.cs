using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.Dtos;

namespace PAD.Backend.Services;

public class ModeloService
{
    private readonly ApplicationDbContext _context;

    public ModeloService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ModeloDto>> ObtenerTodos(string marca)
    {
        var modelos = _context.Modelos.Where(m => string.Equals(m.Marca.Nombre, marca));

        var modelosDto = await modelos
            .Select(m => new ModeloDto { Id = m.Id, Nombre = m.Nombre })
            .ToListAsync();

        return modelosDto;
    }
}
