namespace PAD.Backend.Models.Entidades;

public class Modelo
{
    public int Id { get; set; }

    public int MarcaId { get; set; }
    public Marca Marca { get; set; }

    public string Nombre { get; set; }

    public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}