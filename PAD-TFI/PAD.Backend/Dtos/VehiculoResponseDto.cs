using PAD.Backend.Models.Enums;

namespace PAD.Backend.Dtos
{
    public class VehiculoResponseDto
    {
        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Categoria { get; set; }

        public decimal Precio { get; set; }

        public DateOnly FechaFabricacion { get; set; }

        public string NumeroChasis { get; set; }

        public string NumeroMotor { get; set; }

        public string NumeroPatente { get; set; }   
    }
}
