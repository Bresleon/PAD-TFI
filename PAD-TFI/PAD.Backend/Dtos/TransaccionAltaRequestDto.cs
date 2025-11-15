using PAD.Backend.Models.Enums;

namespace PAD.Backend.Dtos
{
    public class TransaccionAltaRequestDto
    {
        public string Titular { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public CategoriaVehiculo Categoria { get; set; }
        public decimal Precio { get; set; }
        public DateOnly FechaFabricacion { get; set; }
        public string NumeroChasis { get; set; }
        public string NumeroMotor { get; set; }

    }
}
