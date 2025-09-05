using System.Security.Cryptography;

namespace UdemyNewMicroservice.Discount.Api.Features.Discounts
{
    public class DiscountCodeGenerator
    {
        private const string Allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string Generate(int length = 10)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

            char[] buffer = new char[length];
            for (int i = 0; i < length; i++)
            {
                int idx = RandomNumberGenerator.GetInt32(Allowed.Length); // uniform, bias yok
                buffer[i] = Allowed[idx];
            }

            return new string(buffer);
        }
    }
}