using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace PAD.Backend.ThirdPartyServiceCommunication.MercadoPago.Service
{
    public class MercadoPagoService
    {
        private readonly IConfiguration _configuration;
        private const string MpAccessTokenKey = "MP_ACCESS_TOKEN";
        private readonly string frontendLocalUrl = "http://localhost:4200/pagos"; // hay que ver con que puerto se levanta el front en local

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

        public async Task<Preference> CrearPreferenciaPagoTransferenciPatenteAsync(string titulo, decimal monto)
        {
            var item = new PreferenceItemRequest
            {
                Title = titulo,
                Quantity = 1,
                CurrencyId = "ARS",
                UnitPrice = monto
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
            return await client.CreateAsync(request);
        }

    }
}
