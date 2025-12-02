using PAD.Frontend.Models;

namespace PAD.Frontend.Services;

public class MarcaService
{
    private readonly HttpClient _httpClient;

    public MarcaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MarcaDto>> ObtenerMarcas()
    {
        try
        {
            var url = $"https://elva-taxational-crysta.ngrok-free.dev/api/marcas";
            return (await _httpClient.GetFromJsonAsync<List<MarcaDto>>(url))!;
        }
        catch
        {
            return null;
        }
    }
}
