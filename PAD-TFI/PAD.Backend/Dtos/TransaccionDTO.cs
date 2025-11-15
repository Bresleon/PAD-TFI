namespace PAD.Backend.DTOs;

public class TransaccionDTO
{
    public string LinkDePagoMP { get; set; }

    public DateTime FechaTransaccion { get; set; }
    public string? TitularOrigen { get; set; } 
    public string TitularDestino { get; set; } 
    public decimal CostoOperacion { get; set; }
    public string TipoTransaccion { get; set; } 

    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int AnioFabricacion { get; set; } 
    public string NumeroMotor { get; set; }
    public string CategoriaVehiculo { get; set; }

    public string NumeroPatente { get; set; }
    public string EjemplarPatente { get; set; }
}