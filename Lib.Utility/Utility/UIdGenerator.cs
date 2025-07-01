using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lib.Utility
{
    public class UIdGenerator
    {
        private static readonly Lazy<UIdGenerator> _lazy = new Lazy<UIdGenerator>(
            () => new UIdGenerator(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UIdGenerator Instance
        {
            get { return UIdGenerator._lazy.Value; }
        }

        private static readonly Random _random = new Random();
        private static readonly Dictionary<int, StringBuilder> _stringBuilders = new Dictionary<int, StringBuilder>();
        private const string CHARACTERS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private UIdGenerator()
        {
        }
        private static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalMilliseconds;
        }        
        public static string GenerateUId(int length)
        {
            StringBuilder result;
            if (!_stringBuilders.TryGetValue(length, out result))
            {
                result = new StringBuilder();
                _stringBuilders[length] = result;
            }

            result.Clear();

            for (int i = 0; i < length; i++)
            {
                result.Append(CHARACTERS[_random.Next(CHARACTERS.Length)]);
            }

            return $"{result.ToString().ToUpper()}{ConvertToUnixTime(DateTime.Now)}";
        }
    }
}
