using System;
using System.Net;
using Faker;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PaymentTransactionAPI.Tests.Operations;
using PaymentTransactionAPI.Tests.TestInfrastructure.Enums;
using PaymentTransactionAPI.Tests.TestInfrastructure.Extensions;
using PaymentTransactionAPI.Tests.TestInfrastructure.Helpers;
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
            _response = TransactionOperations.SendRequestToCheckTransactionServiceIsRunning();

            BaseOperations.ValidateResponseStatusCode(_response, HttpStatusCode.OK);
        }

        [Then("^on POST request with valid payment transaction to /payment_transactions status code 200 is returned$")]
        public void ThenOnPostRequestWithValidPaymentTransaction_StatusCode200IsReturned()
        {
            var transaction = new PaymentTransaction
            {
                PaymentTransactionObject = new SaleTransactionBody
                {
                    CardNumber = RandomUtility.GenerateRandomValidCardNumber(),
                    Cvv = RandomUtility.GenerateRandomStringNumber(3),
                    ExpirationDate = RandomUtility.GenerateRandomCardExpirationDate(),
                    Amount = RandomNumber.Next(100, 99999).ToString(),
                    Usage = Lorem.GetFirstWord(),
                    TransactionType = TransactionTypeEnum.Sale.ToDetailedString(),
                    CardHolder = Name.FullName(),
                    Email = Internet.Email(),
                    Address = $"{Address.Country()}, {Address.City()}, {Address.StreetName()}"
                }
            };

            _response = TransactionOperations.SendRequestToCreatePaymentTransaction(transaction);

            BaseOperations.ValidateResponseStatusCode(_response, HttpStatusCode.OK);

            TransactionOperations.ValidateSaleTransactionIsApproved(_response);
        }

        [Then("^on POST request with valid void transaction to /payment_transactions status code 200 is returned$")]
        public void ThenOnPostRequestWithValidVoidTransaction_StatusCode200IsReturned()
        {
            var transaction = new PaymentTransaction
            {
                PaymentTransactionObject = new SaleTransactionBody
                {
                    CardNumber = RandomUtility.GenerateRandomValidCardNumber(),
                    Cvv = RandomUtility.GenerateRandomStringNumber(3),
                    ExpirationDate = RandomUtility.GenerateRandomCardExpirationDate(),
                    Amount = RandomNumber.Next(100, 99999).ToString(),
                    Usage = Lorem.GetFirstWord(),
                    TransactionType = TransactionTypeEnum.Sale.ToDetailedString(),
                    CardHolder = Name.FullName(),
                    Email = Internet.Email(),
                    Address = $"{Address.Country()}, {Address.City()}, {Address.StreetName()}"
                }
            };

            _response = TransactionOperations.SendRequestToCreatePaymentTransaction(transaction);

            var voidTransaction = new PaymentTransaction()
            {
                PaymentTransactionObject = new VoidTransactionBody()
                {
                    ReferenceId = BaseOperations.GetJsonKeyFromResponse(_response, "unique_id"),
                    TransactionType = TransactionTypeEnum.Void.ToDetailedString()
                }
            };

            _response = TransactionOperations.SendRequestToCreateVoidTransaction(voidTransaction);

            BaseOperations.ValidateResponseStatusCode(_response, HttpStatusCode.OK);

            TransactionOperations.ValidateVoidTransactionIsApproved(_response);
        }

        [Then("^on POST request with valid transaction and invalid authentication to /payment_transactions status code 200 is returned$")]
        public void ThenOnPostRequestWithValidTransactionAndInvalidAuthentication_StatusCode200IsReturned()
        {
            var transaction = new PaymentTransaction
            {
                PaymentTransactionObject = new SaleTransactionBody
                {
                    CardNumber = RandomUtility.GenerateRandomValidCardNumber(),
                    Cvv = RandomUtility.GenerateRandomStringNumber(3),
                    ExpirationDate = RandomUtility.GenerateRandomCardExpirationDate(),
                    Amount = RandomNumber.Next(100, 99999).ToString(),
                    Usage = Lorem.GetFirstWord(),
                    TransactionType = TransactionTypeEnum.Sale.ToDetailedString(),
                    CardHolder = Name.FullName(),
                    Email = Internet.Email(),
                    Address = $"{Address.Country()}, {Address.City()}, {Address.StreetName()}"
                }
            };

            _response = TransactionOperations.SendRequestToCreatePaymentTransactionWithInvalidAuthorization(transaction);

            BaseOperations.ValidateResponseStatusCode(_response, HttpStatusCode.Unauthorized);
        }

        [Then("^on POST request with void transaction pointing to a non-existing payment transaction to /payment_transactions status code 422 is returned$")]
        public void ThenOnPostRequestWithVoidTransactionPointingToNonExistingPaymentTransaction_StatusCode422IsReturned()
        {
            var voidTransaction = new PaymentTransaction()
            {
                PaymentTransactionObject = new VoidTransactionBody()
                {
                    ReferenceId = RandomUtility.GenerateRandomStringNumber(32),
                    TransactionType = TransactionTypeEnum.Void.ToDetailedString()
                }
            };

            _response = TransactionOperations.SendRequestToCreateVoidTransaction(voidTransaction);

            BaseOperations.ValidateResponseStatusCode(_response, HttpStatusCode.UnprocessableEntity);

            TransactionOperations.ValidateVoidTransactionIsInvalid(_response);
        }

        [Then("^on POST request with void transaction pointing to an existing void transaction to /payment_transactions status code 422 is returned$")]
        public void ThenOnPostRequestWithVoidTransactionPointingToExistingVoidTransaction_StatusCode422IsReturned()
        {
            var transaction = new PaymentTransaction
            {
                PaymentTransactionObject = new SaleTransactionBody
                {
                    CardNumber = RandomUtility.GenerateRandomValidCardNumber(),
                    Cvv = RandomUtility.GenerateRandomStringNumber(3),
                    ExpirationDate = RandomUtility.GenerateRandomCardExpirationDate(),
                    Amount = RandomNumber.Next(100, 99999).ToString(),
                    Usage = Lorem.GetFirstWord(),
                    TransactionType = TransactionTypeEnum.Sale.ToDetailedString(),
                    CardHolder = Name.FullName(),
                    Email = Internet.Email(),
                    Address = $"{Address.Country()}, {Address.City()}, {Address.StreetName()}"
                }
            };

            _response = TransactionOperations.SendRequestToCreatePaymentTransaction(transaction);

            var voidTransactionOne = new PaymentTransaction()
            {
                PaymentTransactionObject = new VoidTransactionBody()
                {
                    ReferenceId = BaseOperations.GetJsonKeyFromResponse(_response, "unique_id"),
                    TransactionType = TransactionTypeEnum.Void.ToDetailedString()
                }
            };

            _response = TransactionOperations.SendRequestToCreateVoidTransaction(voidTransactionOne);

            BaseOperations.ValidateResponseStatusCode(_response, HttpStatusCode.OK);

            TransactionOperations.ValidateVoidTransactionIsApproved(_response);

            var voidTransactionTwo = new PaymentTransaction()
            {
                PaymentTransactionObject = new VoidTransactionBody()
                {
                    ReferenceId = BaseOperations.GetJsonKeyFromResponse(_response, "unique_id"),
                    TransactionType = TransactionTypeEnum.Void.ToDetailedString()
                }
            };

            _response = TransactionOperations.SendRequestToCreateVoidTransaction(voidTransactionTwo);

            BaseOperations.ValidateResponseStatusCode(_response, HttpStatusCode.UnprocessableEntity);

            TransactionOperations.ValidateVoidTransactionIsInvalid(_response);
        }
    }
}
