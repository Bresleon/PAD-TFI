namespace PAD.Frontend.Dtos
{
    public class TransaccionAltaRequestDto
    {
        public string Titular { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Categoria { get; set; }    // string en lugar del enum
        public decimal Precio { get; set; }
        public DateTime FechaFabricacion { get; set; } // MVC usa DateTime
        public string NumeroChasis { get; set; }
        public string NumeroMotor { get; set; }
    }
}
