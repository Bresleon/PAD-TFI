namespace PAD.Backend.Dtos
{
    public class TransaccionAltaRequestDto
    {
        public DateTime FechaTransaccion { get; set; }
        public string Titular { get; set; }
        public int VehiculoId { get; set; }

    }
}
