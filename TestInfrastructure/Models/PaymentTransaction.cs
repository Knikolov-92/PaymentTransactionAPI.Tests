using Newtonsoft.Json;

namespace PaymentTransactionAPI.Tests.TestInfrastructure.Models
{
    public class PaymentTransaction
    {
        [JsonProperty("payment_transaction", NullValueHandling = NullValueHandling.Ignore)]
        public PaymentTransactionBody? PaymentTransactionObject { get; set; }
    }

    public class PaymentTransactionBody
    {
        [JsonProperty("card_number", NullValueHandling = NullValueHandling.Include)]
        public string? CardNumber { get; set; }

        [JsonProperty("cvv", NullValueHandling = NullValueHandling.Include)]
        public string? Cvv { get; set; }

        [JsonProperty("expiration_date", NullValueHandling = NullValueHandling.Include)]
        public string? ExpirationDate { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Include)]
        public string? Amount { get; set; }

        [JsonProperty("usage", NullValueHandling = NullValueHandling.Include)]
        public string? Usage { get; set; }

        [JsonProperty("transaction_type", NullValueHandling = NullValueHandling.Include)]
        public string? TransactionType { get; set; }

        [JsonProperty("card_holder", NullValueHandling = NullValueHandling.Include)]
        public string? CardHolder { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Include)]
        public string? Email { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Include)]
        public string? Address { get; set; }
    }
}
