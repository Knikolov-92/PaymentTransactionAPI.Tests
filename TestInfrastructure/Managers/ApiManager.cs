using System.Configuration;

namespace PaymentTransactionAPI.Tests.TestInfrastructure.Managers
{
    public static class ApiManager
    {
        public static string GetBaseUrl()
        {
            return ConfigurationManager.AppSettings.Get("BaseUrl");
        }

        public static string GetRootEndpoint()
        {
            return ConfigurationManager.AppSettings.Get("PaymentTransactionsService");
        }

        public static string GetPaymentTransactionsEndpoint()
        {
            return ConfigurationManager.AppSettings.Get("PaymentTransactionsService");
        }

        public static string GetAdminUserName()
        {
            return ConfigurationManager.AppSettings.Get("AdminUserName");
        }

        public static string GetAdminUserPassword()
        {
            return ConfigurationManager.AppSettings.Get("AdminUserPassword");
        }

        public static string GetAuthorizationHeader()
        {
            string credentials = $"{GetAdminUserName()}:{GetAdminUserPassword()}";
            var byteArray = System.Text.Encoding.UTF8.GetBytes(credentials);
            string base64Credentials = System.Convert.ToBase64String(byteArray);

            return $"Basic {base64Credentials}";
        }
    }
}
