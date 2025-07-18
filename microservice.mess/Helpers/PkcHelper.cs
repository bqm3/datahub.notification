using System.Security.Cryptography;
using System.Globalization;
using System.Text;

namespace microservice.mess.Helpers
{
    public static class PkceHelper
    {
        public static string GenerateCodeVerifier()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return Base64UrlEncode(randomBytes);
        }

        public static string GenerateCodeChallenge(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.ASCII.GetBytes(codeVerifier));
            return Base64UrlEncode(hash);
        }

        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }

        public static string NormalizeFileValue(string rawFileValue)
        {
            var keyOrder = new[] { "i", "k", "f", "s", "t", "h" };
            var parts = rawFileValue.Split('&', StringSplitOptions.RemoveEmptyEntries);

            // Tạo dictionary<string, string> để chứa từng key=value
            var dict = new Dictionary<string, string>();

            foreach (var part in parts)
            {
                var idx = part.IndexOf('=');
                if (idx > 0)
                {
                    var key = part.Substring(0, idx);
                    var value = part.Substring(idx + 1);
                    if (!dict.ContainsKey(key))
                    {
                        dict[key] = value;
                    }
                }
            }

            // Ghép lại đúng thứ tự
            var result = string.Join("&", keyOrder
                .Where(k => dict.ContainsKey(k))
                .Select(k => $"{k}={dict[k]}"));

            return result;
        }


        public static string NormalizeCategoryPlaceholder(string category)
        {
            string noDiacritics = RemoveVietnameseDiacritics(category);
            return noDiacritics
                .Trim()
                .ToUpperInvariant()
                .Replace(" ", "_");
        }
        public static string RemoveVietnameseDiacritics(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
            var chars = normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
            return new string(chars.ToArray()).Normalize(NormalizationForm.FormC);
        }

    }
}
