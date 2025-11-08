using PAD.Backend.Models.Enums;

namespace PAD.Backend.Models.Entidades;

public class Patente
{
    public int Id { get; set; }

    public int VehiculoId { get; set; }
    public Vehiculo Vehiculo { get; set; }

    public int TitularId { get; set; }
    public Titular Titular { get; set; }

    public string NumeroPatente { get; set; }
    public EjemplarPatente Ejemplar { get; set; }
    public DateOnly FechaEmision { get; set; }
}
