using Microsoft.EntityFrameworkCore;
using PAD.Backend.Models.Enums;

namespace PAD.Backend.Models.Entidades;

[Index(nameof(NumeroChasis), IsUnique = true)]
[Index(nameof(NumeroMotor), IsUnique = true)]
public class Vehiculo
{
    public int Id { get; set; }

    public int ModeloId { get; set; }
    public Modelo Modelo { get; set; }

    public CategoriaVehiculo Categoria { get; set; }
    public decimal Precio { get; set; }
    public DateOnly FechaFabricacion { get; set; }
    public string NumeroChasis { get; set; }
    public string NumeroMotor { get; set; }
}
