using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PAD.Frontend.Models;
using PAD.Frontend.Services;

namespace PAD.Frontend.Controllers
{
    [IgnoreAntiforgeryToken]
    public class TransaccionController : Controller
    {
        private readonly TransaccionService _transaccionServ;
        private readonly MarcaService _marcaServ;
        private readonly ModeloService _modeloServ;
        private readonly ILogger<TransaccionController> _logger;

        public TransaccionController(TransaccionService transaccionServ,
            MarcaService marcaServ,
            ModeloService modeloServ,
            ILogger<TransaccionController> logger)
        {
            _transaccionServ = transaccionServ;
            _marcaServ = marcaServ;
            _modeloServ = modeloServ;
            _logger = logger;
        }

        public async Task<IActionResult> Generar()
        {
            var marcas = await _marcaServ.ObtenerMarcas();

            var categorias = Enum.GetValues(typeof(CategoriaVehiculo)).Cast<CategoriaVehiculo>();

            var vehiculoVM = new VehiculoViewModel
            {
                Marcas = marcas.Select(m => new SelectListItem
                {
                    Value = m.Nombre,
                    Text = m.Nombre
                }).ToList(),

                Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString()
                }).ToList()
            };

            return View(vehiculoVM);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPersona(string cuil)
        {
            var persona = await _transaccionServ.ObtenerPersonaPorCuil(cuil);

            if (persona is null)
                return NotFound("Persona no encontrada");
            
            return Json(persona);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarModelos(string marca)
        {
            var modelos = await _modeloServ.ObtenerModelos(marca);

            return Json(new { modelos });
        }

        [HttpPost]
        public async Task<IActionResult> GenerarPatente([FromBody] TransaccionAltaRequestDto dto)
        {
            try
            {
                var resultado = await _transaccionServ.GenerarNuevaPatente(dto);
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
                var response = await _transaccionServ.TransferirPatente(request);
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
            var resultado = await _transaccionServ.ObtenerPorRangoAsync(desde, hasta);

            return View("Obtener", resultado);
        }
        public async Task<IActionResult> ObtenerPorDni(string dni)
        {
            var resultado = await _transaccionServ.ObtenerPorDniAsync(dni);
            return View("ObtenerPorDni", resultado);
        }



    }
}