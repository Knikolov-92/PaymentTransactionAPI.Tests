Feature: PaymentTransaction
	Cover /payment_transactions Endpoint

	Background: 

	Given existing Payment Transaction application


	Scenario: valid payment transaction request
	
	Then on POST request with valid payment transaction to /payment_transactions status code 200 is returned


	Scenario: valid void payment transaction request
	
	Then on POST request with valid void transaction to /payment_transactions status code 200 is returned


	Scenario: valid payment transaction AND invalid authentication request
	
	Then on POST request with valid transaction and invalid authentication to /payment_transactions status code 200 is returned


	Scenario: void transaction pointing to a non-existent payment transaction
	
	Then on POST request with void transaction pointing to a non-existing payment transaction to /payment_transactions status code 422 is returned

	Scenario: void transaction pointing to an existent void transaction
	
	Then on POST request with void transaction pointing to an existing void transaction to /payment_transactions status code 422 is returned
