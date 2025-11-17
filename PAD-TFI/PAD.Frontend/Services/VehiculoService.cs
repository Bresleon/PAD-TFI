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
                var url = $"https://localhost:7213/api/vehiculos/obtener-por-pantente?patente={patente}";
                return await _httpClient.GetFromJsonAsync<VehiculoDto>(url);
            }
            catch
            {
                return null;
            }
        }
    }
}
