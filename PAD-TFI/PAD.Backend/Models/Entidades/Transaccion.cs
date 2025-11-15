using PAD.Backend.Models.Enums;

namespace PAD.Backend.Models.Entidades;

public class Transaccion
{
    public int Id { get; set; }

    public int? TitularOrigenId { get; set; }
    public Titular? TitularOrigen { get; set; }

    public int TitularDestinoId { get; set; }
    public Titular TitularDestino { get; set; }

    public int PatenteId { get; set; }
    public Patente Patente { get; set; }

    public DateOnly Fecha { get; set; }
    public TipoTransaccion TipoTransaccion { get; set; }

    public decimal Costo { get; set; }

    public string? ExternalReference { get; set; }
}
