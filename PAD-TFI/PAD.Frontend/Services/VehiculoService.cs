using System.Net.Http;
using System.Net.Http.Json;
using PAD.Frontend.Models;

namespace PAD.Frontend.Services
{
    public class VehiculoService
    {
        private readonly HttpClient _httpClient;

        public VehiculoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<VehiculoDto?> ObtenerPorPatenteAsync(string patente)
        {
            try
            {
                var url = $"https://elva-taxational-crysta.ngrok-free.dev/api/vehiculos/obtener-por-pantente?patente={patente}";
                return await _httpClient.GetFromJsonAsync<VehiculoDto>(url);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<VehiculoDto>> ObtenerVehiculosPorCuil(string cuil)
        {
            try
            {
                var url = $"https://elva-taxational-crysta.ngrok-free.dev/api/titulares/{cuil}/vehiculos";
                return await _httpClient.GetFromJsonAsync<List<VehiculoDto>>(url);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener vehículos: {ex.Message}");
                return new List<VehiculoDto>(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return new List<VehiculoDto>();
            }
        }
    }
}
