using Microsoft.AspNetCore.Mvc;

public class TransaccionController : Controller
{
    public IActionResult Generar()
    {
        return View(); // Busca Views/Transaccion/Generar.cshtml
    }

    public IActionResult Transferencia()
    {
        return View(); // Busca Views/Transaccion/Transferencia.cshtml
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
