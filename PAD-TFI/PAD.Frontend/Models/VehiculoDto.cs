namespace PAD.Frontend.Models
{
    public class VehiculoDto
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaFabricacion { get; set; }
        public string NumeroChasis { get; set; }
        public string NumeroMotor { get; set; }
        public string NumeroPatente { get; set; }
    }
}
