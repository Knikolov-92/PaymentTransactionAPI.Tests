using Newtonsoft.Json;
using NUnit.Framework;
using PaymentTransactionAPI.Tests.TestInfrastructure.Constants;
using PaymentTransactionAPI.Tests.TestInfrastructure.Managers;
using PaymentTransactionAPI.Tests.TestInfrastructure.Models;
using RestSharp;

namespace PaymentTransactionAPI.Tests.Operations
{
    public static class TransactionOperations
    {
        //=====================
        //Request Operations
        //=====================
        public static IRestResponse SendRequestToCheckTransactionServiceIsRunning()
        {
            IRestRequest request = new RestRequest(ApiManager.GetRootEndpoint(), Method.GET);

            return BaseOperations.SendRequest(request);
        }

        public static IRestResponse SendRequestToCreatePaymentTransaction(PaymentTransaction details)
        {
            IRestRequest request = new RestRequest(ApiManager.GetPaymentTransactionsEndpoint(), Method.POST);
            string body = JsonConvert.SerializeObject(details);

            request.AddHeader("Content-Type", "application/json;charset=UTF-8");
            request.AddHeader("Authorization", ApiManager.GetAuthorizationHeader());            
            request.AddJsonBody(body);

            return BaseOperations.SendRequest(request);
        }

        public static IRestResponse SendRequestToCreateVoidTransaction(PaymentTransaction details)
        {
            IRestRequest request = new RestRequest(ApiManager.GetPaymentTransactionsEndpoint(), Method.POST);
            string body = JsonConvert.SerializeObject(details);

            request.AddHeader("Content-Type", "application/json;charset=UTF-8");
            request.AddHeader("Authorization", ApiManager.GetAuthorizationHeader());
            request.AddJsonBody(body);

            return BaseOperations.SendRequest(request);
        }

        public static IRestResponse SendRequestToCreatePaymentTransactionWithInvalidAuthorization(PaymentTransaction details)
        {
            IRestRequest request = new RestRequest(ApiManager.GetPaymentTransactionsEndpoint(), Method.POST);
            string body = JsonConvert.SerializeObject(details);

            request.AddHeader("Content-Type", "application/json;charset=UTF-8");
            request.AddHeader("Authorization", "invalid");
            request.AddJsonBody(body);

            return BaseOperations.SendRequest(request);
        }

        //=====================
        //Assert Operations
        //=====================
        public static void ValidateSaleTransactionIsApproved(IRestResponse response)
        {
            string refNumber = BaseOperations.GetJsonKeyFromResponse(response, "unique_id");
            string status = BaseOperations.GetJsonKeyFromResponse(response, "status");
            string message = BaseOperations.GetJsonKeyFromResponse(response, "message");

            Assert.Multiple(() =>
            {
                Assert.That(refNumber.Length, Is.EqualTo(ExpectedResponses.PAYMENT_TRANSACTION_REFERENCE_ID_LENGTH));
                Assert.That(status, Is.EqualTo(ExpectedResponses.PAYMENT_TRANSACTION_APPROVED_STATUS));
                Assert.That(message, Is.EqualTo(ExpectedResponses.PAYMENT_TRANSACTION_APPROVED_MESSAGE));
            });
        }

        public static void ValidateVoidTransactionIsApproved(IRestResponse response)
        {
            string refNumber = BaseOperations.GetJsonKeyFromResponse(response, "unique_id");
            string status = BaseOperations.GetJsonKeyFromResponse(response, "status");
            string message = BaseOperations.GetJsonKeyFromResponse(response, "message");

            Assert.Multiple(() =>
            {
                Assert.That(refNumber.Length, Is.EqualTo(ExpectedResponses.PAYMENT_TRANSACTION_REFERENCE_ID_LENGTH));
                Assert.That(status, Is.EqualTo(ExpectedResponses.PAYMENT_TRANSACTION_APPROVED_STATUS));
                Assert.That(message, Is.EqualTo(ExpectedResponses.VOID_TRANSACTION_APPROVED_MESSAGE));
            });
        }

        public static void ValidateVoidTransactionIsInvalid(IRestResponse response)
        {
            string refId = BaseOperations.GetJsonKeyFromResponse(response, "reference_id");

            Assert.That(refId.Contains(ExpectedResponses.VOID_TRANSACTION_INVALID_MESSAGE));
        }

        public static void ValidateSaleTransactionIsDeclined(IRestResponse response)
        {
            string refNumber = BaseOperations.GetJsonKeyFromResponse(response, "unique_id");
            string status = BaseOperations.GetJsonKeyFromResponse(response, "status");
            string message = BaseOperations.GetJsonKeyFromResponse(response, "message");

            Assert.Multiple(() =>
            {
                Assert.That(refNumber.Length, Is.EqualTo(ExpectedResponses.PAYMENT_TRANSACTION_REFERENCE_ID_LENGTH));
                Assert.That(status, Is.EqualTo(ExpectedResponses.PAYMENT_TRANSACTION_DECLINED_STATUS));
                Assert.That(message, Is.EqualTo(ExpectedResponses.PAYMENT_TRANSACTION_DECLINED_MESSAGE));
            });
        }
    }
}
