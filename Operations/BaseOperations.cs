using PaymentTransactionAPI.Tests.TestInfrastructure.Managers;
using RestSharp;
using System;

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
    }
}
