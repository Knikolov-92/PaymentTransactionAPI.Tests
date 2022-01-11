﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PaymentTransactionAPI.Tests.TestInfrastructure.Constants;
using PaymentTransactionAPI.Tests.TestInfrastructure.Managers;
using PaymentTransactionAPI.Tests.TestInfrastructure.Models;
using RestSharp;

namespace PaymentTransactionAPI.Tests.Operations
{
    public static class TransactionOperations
    {
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
    }
}
