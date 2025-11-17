namespace PAD.Frontend.Models
{
    public class TransaccionAltaRequestDto
    {
        public string Titular { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Categoria { get; set; }

        public decimal Precio { get; set; }
        public DateOnly FechaFabricacion { get; set; } 
        public string NumeroChasis { get; set; }
        public string NumeroMotor { get; set; }
    }
}
