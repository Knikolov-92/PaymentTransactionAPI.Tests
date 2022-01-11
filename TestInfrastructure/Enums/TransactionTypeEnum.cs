using System.ComponentModel;

namespace PaymentTransactionAPI.Tests.TestInfrastructure.Enums
{
    public enum TransactionTypeEnum
    {
        [Description("sale")] Sale,
        [Description("void")] Void
    }
}
