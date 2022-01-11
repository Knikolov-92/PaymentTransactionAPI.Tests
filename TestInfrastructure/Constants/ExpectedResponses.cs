
namespace PaymentTransactionAPI.Tests.TestInfrastructure.Constants
{
    public static class ExpectedResponses
    {
        public const string PAYMENT_TRANSACTION_APPROVED_STATUS = "approved";

        public const string PAYMENT_TRANSACTION_APPROVED_MESSAGE = "Your transaction has been approved.";

        public const string VOID_TRANSACTION_APPROVED_MESSAGE = "Your transaction has been voided successfully";

        public const string VOID_TRANSACTION_INVALID_MESSAGE = "Invalid reference transaction!";

        public const int PAYMENT_TRANSACTION_REFERENCE_ID_LENGTH = 32;
    }
}
