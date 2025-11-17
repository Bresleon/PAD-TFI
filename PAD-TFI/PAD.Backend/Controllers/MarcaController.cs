using Microsoft.AspNetCore.Mvc;
using PAD.Backend.Dtos;
using PAD.Backend.Services;

namespace PAD.Backend.Controllers;

[Route("api/marcas")]
[ApiController]
public class MarcaController : ControllerBase
{
    private readonly MarcaService _marcaService;

    public MarcaController(MarcaService marcaService)
    {
        _marcaService = marcaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MarcaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerMarcas()
    {
        var marcas = await _marcaService.ObtenerTodos();
        return Ok(marcas);
    }
}
