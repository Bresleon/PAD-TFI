using PAD.Frontend.Models;

namespace PAD.Frontend.Services;

public class ModeloService
{
    private readonly HttpClient _httpClient;

    public ModeloService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ModeloDto>> ObtenerModelos(string marca)
    {
        try
        {
            var url = $"https://elva-taxational-crysta.ngrok-free.dev/api/modelos/{marca}";
            return (await _httpClient.GetFromJsonAsync<List<ModeloDto>>(url))!;
        }
        catch
        {
            return null;
        }
    }
}
