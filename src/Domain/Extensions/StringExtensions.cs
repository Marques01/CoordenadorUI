using System.Text.RegularExpressions;

namespace Domain.Extensions
{
    public static class StringExtensions
    {
        public static string CleanInput(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var regex = new Regex(@"[^a-zA-Z0-9\s\u00C0-\u00FF]");

            string cleanValue = regex.Replace(input, string.Empty);

            return cleanValue.Trim();
        }

        public static string FormatCPF(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return string.Empty;

            // Remove qualquer caractere que não seja número
            cpf = Regex.Replace(cpf, @"[^\d]", "");

            if (cpf.Length != 11)
                return cpf; // Retorna o CPF original se não tiver 11 dígitos

            // Formata o CPF
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public static string FormatCellPhoneNumber(this string number)
        {
            // Remove todos os caracteres não numéricos
            number = Regex.Replace(number, @"\D", "");

            // Verifica se o número tem 11 dígitos
            if (number.Length == 11)
                return Regex.Replace(number, @"(\d{2})(\d{5})(\d{4})", "($1) $2-$3");

            throw new ArgumentException("O número de celular deve conter 11 dígitos.");
        }

        public static string CapitalizeFirstLetters(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var words = text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }

            return string.Join(" ", words);
        }

        public static bool IsValidCNPJ(this string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            var digitsOnly = Regex.Replace(cnpj, @"\D", "");
            if (digitsOnly.Length != 14)
                return false;

            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj = digitsOnly.Substring(0, 12);
            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

            int remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            string digit = remainder.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

            remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit = digit + remainder.ToString();

            return digitsOnly.EndsWith(digit);
        }

        public static bool IsRootCNPJMatching(this string cnpj, string rootCnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || string.IsNullOrWhiteSpace(rootCnpj))
                return false;

            var cnpjDigits = Regex.Replace(cnpj, @"\D", "");
            var rootCnpjDigits = Regex.Replace(rootCnpj, @"\D", "");

            if (cnpjDigits.Length != 14 || rootCnpjDigits.Length != 8)
                return false;

            return cnpjDigits.StartsWith(rootCnpjDigits);
        }

        public static string FormatCNPJ(this string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return string.Empty;

            var digitsOnly = Regex.Replace(cnpj, @"\D", string.Empty);
            if (digitsOnly.Length != 14)
                return cnpj; // Return the original CNPJ if it doesn't have 14 digits

            return Regex.Replace(digitsOnly, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
        }
    }
}
