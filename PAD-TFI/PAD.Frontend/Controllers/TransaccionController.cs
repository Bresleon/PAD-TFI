using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PAD.Frontend.Models;
using PAD.Frontend.Services;
using System.Net.Http;

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

        [HttpGet]
        public IActionResult Transferencia()
        {
            return View(new TransaccionTransferenciaRequestDto());
        }

        [HttpPost]
        public async Task<IActionResult> Transferencia([FromBody] TransaccionTransferenciaRequestDto request)
        {
            try
            {
                var response = await _service.TransferirPatente(request);
                return Json(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Obtener()
        {
            return View(new List<TransaccionDto>());
        }

        public async Task<IActionResult> Obtener(DateTime desde, DateTime? hasta)
        {
            var resultado = await _service.ObtenerPorRangoAsync(desde, hasta);

            return View("Obtener", resultado);
        }



        public IActionResult ObtenerPorDni()
        {
            return View();
        }

    }
}