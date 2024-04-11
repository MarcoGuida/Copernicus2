using System.Text.RegularExpressions;

namespace Copernicus2
{
    //Clase Auxiliar para dejar limpio el código del controlador

        internal static class Tools
        {

            internal static bool IsValidEmail(string email)
            {
                // Expresión regular para validar el formato de un correo electrónico
                string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

                // Verificar si el email coincide con el patrón de la expresión regular
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                return Regex.IsMatch(email, emailPattern);
            }

            internal static bool IsValidIso8601DateTime(string dateTimeString)
            {
                DateTimeOffset result;
                bool isValid = DateTimeOffset.TryParseExact(
                    dateTimeString,
                    "yyyy-MM-ddTHH:mm:ss.fffZ",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal,
                    out result);

                return isValid;
            }

            internal static bool IsValidCountry(string countryName)
            {
                
                return ValidCountryNames.Contains(FirstCharToUpper(countryName).Trim(), StringComparer.OrdinalIgnoreCase);
            }

            internal static bool IsValidString(string texto)
            {
                // Patrón de expresión regular para permitir solo letras y algunos caracteres especiales
                string patron = @"^[a-zA-ZáéíóúüñÁÉÍÓÚÜÑ\s]+$";

                // Crear una instancia de Regex con el patrón
                Regex regex = new Regex(patron);

                // Verificar si la cadena coincide con el patrón
                return regex.IsMatch(texto);
            }

        private static readonly HashSet<string> ValidCountryNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
            "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Antigua and Barbuda", "Argentina", "Armenia", "Australia", "Austria", "Azerbaijan",
            "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Brazil", "Brunei", "Bulgaria", "Burkina Faso", "Burundi",
            "Cabo Verde", "Cambodia", "Cameroon", "Canada", "Central African Republic", "Chad", "Chile", "China", "Colombia", "Comoros", "Congo", "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic",
            "Denmark", "Djibouti", "Dominica", "Dominican Republic",
            "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Eswatini", "Ethiopia",
            "Fiji", "Finland", "France",
            "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Greece", "Grenada", "Guatemala", "Guinea", "Guinea-Bissau", "Guyana",
            "Haiti", "Honduras", "Hungary",
            "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Ivory Coast",
            "Jamaica", "Japan", "Jordan",
            "Kazakhstan", "Kenya", "Kiribati", "Kuwait", "Kyrgyzstan",
            "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg",
            "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius", "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar",
            "Namibia", "Nauru", "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger", "Nigeria", "North Korea", "North Macedonia", "Norway",
            "Oman",
            "Pakistan", "Palau", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal",
            "Qatar",
            "Romania", "Russia", "Rwanda",
            "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Korea", "South Sudan", "Spain", "Sri Lanka", "Sudan", "Suriname", "Sweden", "Switzerland", "Syria",
            "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor-Leste", "Togo", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Tuvalu",
            "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "Uruguay", "Uzbekistan",
            "Vanuatu", "Vatican City", "Venezuela", "Vietnam",
            "Yemen",
            "Zambia", "Zimbabwe"
        };

        internal static string FirstCharToUpper(string text)
        {
            text = text.ToLower();
            return char.ToUpper(text[0]) + text.Substring(1);
        }

    }

    
}
