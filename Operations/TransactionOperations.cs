using Newtonsoft.Json;
using PaymentTransactionAPI.Tests.TestInfrastructure.Managers;
using PaymentTransactionAPI.Tests.TestInfrastructure.Models;
using RestSharp;

namespace PaymentTransactionAPI.Tests.Operations
{
    public static class TransactionOperations
    {       

        public static IRestResponse SendRequestToCreatePaymentTransaction(PaymentTransaction details)
        {
            IRestRequest request = new RestRequest(ApiManager.GetPaymentTransactionsEndpoint(), Method.POST);
            string body = JsonConvert.SerializeObject(details);

            request.AddHeader("Content-Type", "application/json;charset=UTF-8");
            request.AddHeader("Authorization", ApiManager.GetAuthorizationHeader());            
            request.AddJsonBody(body);

            return BaseOperations.SendRequest(request);
        }

        public static IRestResponse SendRequestToCheckTransactionServiceIsRunning()
        {
            IRestRequest request = new RestRequest(ApiManager.GetRootEndpoint(), Method.GET);

            return BaseOperations.SendRequest(request);
        }
    }
}
