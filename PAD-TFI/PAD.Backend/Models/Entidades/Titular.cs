namespace PAD.Backend.Models.Entidades;

public class Titular
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Dni { get; set; }
    public string? Cuil { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }

    public ICollection<Patente> Patentes { get; set; } = new List<Patente>();
    public ICollection<Transaccion> Transacciones { get; set;} = new List<Transaccion>();
}
