using Newtonsoft.Json;
using PAD.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Backend error: {raw}");
            }

            return JsonSerializer.Deserialize<TransaccionResponseDto>(raw, _jsonOptions);
        }
        public async Task<TransaccionTransferenciaResponseDto> TransferirPatente(TransaccionTransferenciaRequestDto dto)
        {
            var url = $"https://localhost:7213/api/transacciones/transferir-patente";
            try
            {
                var response = await _http.PostAsJsonAsync(url, dto);
                Console.WriteLine("respuesta TRANSFERENCIA desde servicio --> " + response);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Backend error Transferencia --> " + error);
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<TransaccionTransferenciaResponseDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en TransferirPatente: " + ex.Message);
                return null;
            }
        }

        public async Task<List<TransaccionDto>> ObtenerPorRangoAsync(DateTime desde, DateTime? hasta)
        {
            string url = $"https://localhost:7213/api/transacciones/obtener-por-rango?desde={desde:yyyy-MM-dd}";

            if (hasta.HasValue)
                url += $"&hasta={hasta:yyyy-MM-dd}";

            
            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<TransaccionDto>();

            return await response.Content.ReadFromJsonAsync<List<TransaccionDto>>();
        }

        public async Task<List<TransaccionDto>> ObtenerPorDniAsync(string dni)
        {
            string url = $"https://localhost:7213/api/transacciones/obtener-por-dni?dni={dni}";
            Console.WriteLine("URL usada --> " + url);

            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<TransaccionDto>();

            return await response.Content.ReadFromJsonAsync<List<TransaccionDto>>();
        }




    }
}
