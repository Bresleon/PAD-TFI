using PAD.Backend.Models.Enums;

namespace PAD.Backend.Models.Entidades;

public class Vehiculo
{
    public int Id { get; set; }

    public int MarcaId { get; set; }
    public Marca Marca { get; set; }

    public int ModeloId { get; set; }
    public Modelo Modelo { get; set; }

    public CategoriaVehiculo Categoria { get; set; }
    public decimal Precio { get; set; }
    public DateOnly FechaFabricacion { get; set; }
    public string NumeroChasis { get; set; }
    public string NumeroMotor { get; set; }
}
