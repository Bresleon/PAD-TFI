using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using PAD.Frontend.Models;

namespace PAD.Frontend.Services
{
    public class TransaccionService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions;

        public TransaccionService(HttpClient http)
        {
            _http = http;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<PersonaResponseDto?> ObtenerPersonaPorCuil(string cuil)
        {
            var url = $"https://localhost:7213/api/personas/por-cuil/{cuil}";
            return await _http.GetFromJsonAsync<PersonaResponseDto>(url, _jsonOptions);
        }

        public async Task<TransaccionResponseDto?> GenerarNuevaPatente(TransaccionAltaRequestDto dto)
        {
            var url = $"https://localhost:7213/api/transacciones/generar-nueva-patente";

            var response = await _http.PostAsJsonAsync(url, dto);

            var raw = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Backend dice:", response);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Backend error: {raw}");
            }

            return JsonSerializer.Deserialize<TransaccionResponseDto>(raw, _jsonOptions);
        }

    }
}
