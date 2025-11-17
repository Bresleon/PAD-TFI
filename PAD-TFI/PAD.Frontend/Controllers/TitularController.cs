using Microsoft.AspNetCore.Mvc;
using PAD.Frontend.Services;

namespace PAD.Frontend.Controllers
{
    public class TitularController : Controller
    {
        private readonly TitularService _service;

        public TitularController(TitularService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult ObtenerTitular()
        {
            return View("~/Views/Titular/ObtenerTitular.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPorCuil(string cuil)
        {
            Console.WriteLine("CUIL recibido en el controlador --> " + cuil);

            var titular = await _service.ObtenerPorCuilAsync(cuil);

            if (titular == null)
            {
                ViewBag.Error = "No se encontró ni se pudo crear un titular con ese CUIL.";
                return View("ObtenerTitular");
            }

            return View("ObtenerTitular", titular); // <-- ESTA ES LA CORRECTA
        }

    }

}
