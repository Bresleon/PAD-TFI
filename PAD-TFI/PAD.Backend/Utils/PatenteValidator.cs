using System.Text.RegularExpressions;

namespace PAD.Backend.Utils
{
    public static class PatenteValidator
    {
        public static bool EsPatenteArgentinaValida(string patente)
        {
            if (string.IsNullOrWhiteSpace(patente))
            {
                return false;
            }

            string patenteLimpia = patente
                .Replace(" ", "")
                .Replace("-", "")
                .Replace(".", "")
                .ToUpper();

            string regexPatente = @"^([A-Z]{3}[0-9]{3}|[A-Z]{2}[0-9]{3}[A-Z]{2})$";

            return Regex.IsMatch(patenteLimpia, regexPatente);
        }
    }
}
