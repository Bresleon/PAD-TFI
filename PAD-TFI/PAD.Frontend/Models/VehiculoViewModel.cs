using Microsoft.AspNetCore.Mvc.Rendering;

namespace PAD.Frontend.Models;

public class VehiculoViewModel
{
    public List<SelectListItem> Marcas { get; set; } = new();
    public List<SelectListItem> Categorias { get; set; } = new();
}
