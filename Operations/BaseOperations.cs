using NUnit.Framework;
using PaymentTransactionAPI.Tests.TestInfrastructure.Managers;
using RestSharp;
using System;
using System.Net;

namespace PaymentTransactionAPI.Tests.Operations
{
    public static class BaseOperations
    {
        public static IRestResponse SendRequest(IRestRequest request)
        {
            IRestClient client = new RestClient()
            {
                BaseUrl = new Uri(ApiManager.GetBaseUrl())
            };

            return client.Execute(request);
        }

        public static void ValidateResponseStatusCode(IRestResponse response, HttpStatusCode expectedStatusCode)
        {
            Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode));
        }
    }
}
