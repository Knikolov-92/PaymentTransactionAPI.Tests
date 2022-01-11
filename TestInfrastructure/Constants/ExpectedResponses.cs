﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentTransactionAPI.Tests.TestInfrastructure.Constants
{
    public static class ExpectedResponses
    {
        public const string PAYMENT_TRANSACTION_APPROVED_STATUS = "approved";

        public const string PAYMENT_TRANSACTION_APPROVED_MESSAGE = "Your transaction has been approved.";

        public const int PAYMENT_TRANSACTION_REFERENCE_ID_LENGTH = 32;
    }
}