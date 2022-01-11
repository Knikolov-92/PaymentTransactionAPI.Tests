using PaymentTransactionAPI.Tests.TestInfrastructure.Enums;
using System.ComponentModel;

namespace PaymentTransactionAPI.Tests.TestInfrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDetailedString(this TransactionTypeEnum val)
        {
            return ToDetailedString((object)val);
        }

        private static string ToDetailedString(object val)
        {
            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
