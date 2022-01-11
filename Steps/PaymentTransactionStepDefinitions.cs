using System;
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
            
        }

        [Then("^on POST request with valid payment transaction to /payment_transactions status code 200 is returned$")]
        public void ThenOnPostRequestWithValidPaymentTransaction_StatusCode200IsReturned()
        {
           
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
