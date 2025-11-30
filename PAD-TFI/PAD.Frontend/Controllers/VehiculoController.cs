using Microsoft.AspNetCore.Mvc;
using PAD.Frontend.Services;
using PAD.Frontend.Models;

namespace PAD.Frontend.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly VehiculoService _service;

        public VehiculoController(VehiculoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult ObtenerVehiculo()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPorPatente(string patente)
        {
            var vehiculo = await _service.ObtenerPorPatenteAsync(patente);

            if (vehiculo == null)
            {
                ViewBag.Error = "No se encontró un vehículo con esa patente.";
                return View("ObtenerVehiculo");
            }

            return View("ObtenerVehiculo", vehiculo);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerPorCuil(string cuil)
        {
            if (string.IsNullOrEmpty(cuil))
            {
                return View("ListadoPorCuil");
            }

            try
            {
                var vehiculos = await _service.ObtenerVehiculosPorCuil(cuil);
                return View("ListadoPorCuil", vehiculos ?? new List<VehiculoDto>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al buscar vehículos: {ex.Message}";
                return View("ListadoPorCuil", new List<VehiculoDto>());
            }
        }

    }
}
