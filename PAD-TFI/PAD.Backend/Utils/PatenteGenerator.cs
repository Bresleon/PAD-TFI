using System.Text;

namespace PAD.Backend.Utils
{
    public static class PatenteGenerator
    {
        private static readonly Random Random = new Random();
        private const string Letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Numeros = "0123456789";

        public static string GenerarPatente()
        {
            var parteLetras1 = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                parteLetras1.Append(Letras[Random.Next(Letras.Length)]);
            }

            var parteNumeros = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                parteNumeros.Append(Numeros[Random.Next(Numeros.Length)]);
            }

            var parteLetras2 = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                parteLetras2.Append(Letras[Random.Next(Letras.Length)]);
            }

            return $"{parteLetras1} {parteNumeros} {parteLetras2}";
        }
    }
}
