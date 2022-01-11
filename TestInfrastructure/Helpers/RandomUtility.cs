using System;

namespace PaymentTransactionAPI.Tests.TestInfrastructure.Helpers
{
    public static class RandomUtility
    {
        public static string GenerateRandomStringNumber(int numberOfDigits)
        {
            var chars = "0123456789";
            var numberString = new char[numberOfDigits];
            var random = new Random();

            for (int i = 0; i < numberString.Length; i++)
            {
                numberString[i] = chars[random.Next(chars.Length)];
            }

            return new string(numberString);
        }

        public static string GenerateRandomCardExpirationDate()
        {
            var random = new Random();
            string month = random.Next(1, 12).ToString();
            string year = random.Next(2000, 2099).ToString();

            if (int.Parse(month) < 10)
            {
                month = $"0{month}";
            }

            return $"{month}/{year}";
        }

        public static string GenerateRandomValidCardNumber()
        {
            return "4200000000000000";
        }
    }
}
