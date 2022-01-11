using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PaymentTransactionAPI.Tests.TestInfrastructure.Managers;
using PaymentTransactionAPI.Tests.TestInfrastructure.Models;
using RestSharp;
using TechTalk.SpecFlow;

namespace PaymentTransactionAPI.Tests.Steps
{
    [Binding]
    public sealed class PaymentTransactionStepDefinitions
    {        
        private IRestClient? _client;
        private IRestRequest? _request;
        private IRestResponse? _response;

        [Given("^existing Payment Transaction application$")]
        public void GivenExistingPaymentTransactionApplication()
        {
            _client = new RestClient()
            {
                BaseUrl = new Uri(ApiManager.GetBaseUrl())
            };

            _request = new RestRequest(ApiManager.GetRootEndpoint(), Method.GET);
            _response = _client.Execute(_request);

            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then("^on POST request with valid payment transaction to /payment_transactions status code 200 is returned$")]
        public void ThenOnPostRequestWithValidPaymentTransaction_StatusCode200IsReturned()
        {
            _request = new RestRequest(ApiManager.GetPaymentTransactionsEndpoint(), Method.POST);
            _request.AddHeader("Content-Type", "application/json;charset=UTF-8");
            _request.AddHeader("Authorization", ApiManager.GetAuthorizationHeader());

            var transaction = new PaymentTransaction
            {
                PaymentTransactionObject = new SaleTransactionBody()
                {
                    CardNumber = "4200000000000000",
                    Cvv = "123",
                    ExpirationDate = "01/2023",
                    Amount = "1000",
                    Usage = "Tax",
                    TransactionType = "sale",
                    CardHolder = "Ivancho",
                    Email = "ivancho@isthebest.com",
                    Address = "Ivanolandia, Ivanov street 123"
                }
            };
            var body = JsonConvert.SerializeObject(transaction);

            _request.AddJsonBody(body);
            _response = _client.Execute(_request);

            var json = JObject.Parse(_response.Content);
            string refNumber = json.GetValue("unique_id").ToString().Trim();
            string status = json.GetValue("status").ToString().Trim();
            string message = json.GetValue("message").ToString().Trim();

            Assert.Multiple(() =>
            {
                Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(refNumber.Length, Is.EqualTo(32));
                Assert.That(status, Is.EqualTo("approved"));
                Assert.That(message, Is.EqualTo("Your transaction has been approved."));
            });
        }

        [Then("^on POST request with valid void transaction to /payment_transactions status code 200 is returned$")]
        public void ThenOnPostRequestWithValidVoidTransaction_StatusCode200IsReturned()
        {

        }

        [Then("^on POST request with valid transaction and invalid authentication to /payment_transactions status code 200 is returned$")]
        public void ThenOnPostRequestWithValidTransactionAndInvalidAuthentication_StatusCode200IsReturned()
        {

        }

        [Then("^on POST request with void transaction pointing to a non-existing payment transaction to /payment_transactions status code 422 is returned$")]
        public void ThenOnPostRequestWithVoidTransactionPointingToNonExistingPaymentTransaction_StatusCode422IsReturned()
        {

        }

        [Then("^on POST request with void transaction pointing to an existing void transaction to /payment_transactions status code 422 is returned$")]
        public void ThenOnPostRequestWithVoidTransactionPointingToExistingVoidTransaction_StatusCode422IsReturned()
        {

        }
    }
}
