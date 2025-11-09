using System.Net.Http.Json; 

public class RenaperService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string ClientName = "RenaperClient"; 

    public RenaperService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PersonaRenaperDto?> ObtenerPersonaPorCuilAsync(string cuil)
    {

        var client = _httpClientFactory.CreateClient(ClientName);

        string requestUri = $"api/v1/persona?cuil={cuil}";

        try
        {
            var persona = await client.GetFromJsonAsync<PersonaRenaperDto>(requestUri);
            return persona;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al consultar RENAPER: {ex.Message}");
            return null; 
        }
    }
}