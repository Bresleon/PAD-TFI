using PAD.Frontend.Models;

namespace PAD.Frontend.Services
{
    public class TitularService
    {
        private readonly HttpClient _http;

        public TitularService(HttpClient http)
        {
            _http = http;
        }

        public async Task<TitularDto?> ObtenerPorCuilAsync(string cuil)
        {
            string url = $"https://localhost:7213/api/titulares/obtener-por-cuil?cuil={cuil}";
            Console.WriteLine("URL usada --> " + url);

            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<TitularDto>();
        }
    }

}
