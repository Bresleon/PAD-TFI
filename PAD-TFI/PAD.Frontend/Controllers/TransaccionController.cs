using Microsoft.AspNetCore.Mvc;
using PAD.Frontend.Services;
using PAD.Frontend.Models;

namespace PAD.Frontend.Controllers
{
    [IgnoreAntiforgeryToken]
    public class TransaccionController : Controller
    {
        private readonly TransaccionService _service;
        private readonly ILogger<TransaccionController> _logger;

        public TransaccionController(TransaccionService service, ILogger<TransaccionController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult Generar()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPersona(string cuil)
        {
            var persona = await _service.ObtenerPersonaPorCuil(cuil);

            if (persona is null)
                return NotFound("Persona no encontrada");

            return Json(persona);
        }

        [HttpPost]
        public async Task<IActionResult> GenerarPatente([FromBody] TransaccionAltaRequestDto dto)
        {
            try
            {
                var resultado = await _service.GenerarNuevaPatente(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR DESDE API --> " + ex.Message);
                return BadRequest(ex.Message);
            }
        }




        public IActionResult Transferencia()
        {
            return View();
        }

        public IActionResult Obtener()
        {
            return View();
        }

        public IActionResult ObtenerPorDni()
        {
            return View();
        }

    }
}