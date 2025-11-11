using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RenaperController : ControllerBase
{
    private readonly RenaperService _renaperService;

    public RenaperController(RenaperService renaperService)
    {
        _renaperService = renaperService;
    }

    [HttpGet("persona/{cuil}")]
    public async Task<IActionResult> GetPersonaFromRenaper(string cuil)
    {
        var persona = await _renaperService.ObtenerPersonaPorCuilAsync(cuil);

        if (persona == null)
        {
            return NotFound($"No se encontró la persona con CUIL {cuil} o el servicio externo falló.");
        }

        return Ok(persona);
    }
}