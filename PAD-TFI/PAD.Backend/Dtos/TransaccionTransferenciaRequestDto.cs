namespace PAD.Backend.Dtos
{
    public class TransaccionTransferenciaRequestDto
    {
        public string TitularOrigen { get; set; }
        public string TitularDestino { get; set; }
        public int VehiculoId { get; set; }
    }
}
