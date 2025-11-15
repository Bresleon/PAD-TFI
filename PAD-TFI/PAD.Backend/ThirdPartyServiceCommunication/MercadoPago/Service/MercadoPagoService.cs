using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace PAD.Backend.ThirdPartyServiceCommunication.MercadoPago.Service
{
    public class MercadoPagoService
    {
        private readonly IConfiguration _configuration;
        private const string MpAccessTokenKey = "MP_ACCESS_TOKEN";

        public MercadoPagoService(IConfiguration configuration)
        {
            
            _configuration = configuration; 

            string accessToken = _configuration[MpAccessTokenKey]
                                ?? throw new InvalidOperationException($"La clave de configuración '{MpAccessTokenKey}' no se encontró.");

            MercadoPagoConfig.AccessToken = accessToken;
        }
        public async Task<Preference> CrearPreferenciaPagoAltaPatenteAsync(string patenteNumero)
        {
            decimal montoPrueba = 1.00m;

            string frontendLocalUrl = "http://localhost:4200/pagos"; // hay que ver con que puerto se levanta el front en local

            var item = new PreferenceItemRequest
            {
                Title = $"Alta de Patente Mercosur - {patenteNumero}",
                Quantity = 1,
                CurrencyId = "ARS", 
                UnitPrice = montoPrueba
            };

            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest> { item },

                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = $"{frontendLocalUrl}/exito",
                    Pending = $"{frontendLocalUrl}/pendiente",
                    Failure = $"{frontendLocalUrl}/fallido"
                },
                AutoReturn = "approved" 
            };

            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);

            return preference;
        }
    }
}
