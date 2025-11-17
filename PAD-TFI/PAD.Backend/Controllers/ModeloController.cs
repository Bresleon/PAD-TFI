using Microsoft.AspNetCore.Mvc;
using PAD.Backend.Dtos;
using PAD.Backend.Services;

namespace PAD.Backend.Controllers;

[Route("api/modelos")]
[ApiController]
public class ModeloController : ControllerBase
{
    private readonly ModeloService _modeloService;

    public ModeloController(ModeloService modeloService)
    {
        _modeloService = modeloService;
    }

    [HttpGet("{marca}")]
    [ProducesResponseType(typeof(List<ModeloDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerModelos(string marca)
    {
        var modelos = await _modeloService.ObtenerTodos(marca.Trim());
        return Ok(modelos);
    }
}
