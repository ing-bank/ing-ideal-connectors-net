namespace iDealAdvancedConnector
{
    public class IDealConnectorOptions
    {
        /// <summary>
        /// Base64 encoded string of the ClientCertificate
        /// </summary>
        /// <value></value>
        public string ClientCertificate { get; set; }

        /// <summary>
        /// Password used to protected the ClientCertificate (if applicable)
        /// </summary>
        /// <value></value>
        public string ClientCertificatePassword { get; set; }

        /// <summary>
        /// Base64 encoded string of the AcquirerCertificate
        /// </summary>
        /// <value></value>
        public string AcquirerCertificate { get; set; }

        /// <summary>
        /// Acquirer timeout in seconds. Note that this isn't handled automatically by this library
        /// A user of the connector has to configure the passed in HttpClient with that value himself
        /// </summary>
        /// <value></value>
        public string AcquirerTimeout { get; set; }
        public string MerchantId { get; set; }
        public string SubId { get; set; }
        public string ExpirationPeriod { get; set; }
        public string MerchantReturnURL { get; set; }
        public string AcquirerURL { get; set; }
        public string AcquirerDirectoryURL { get; set; }
        public string AcquirerTransactionURL { get; set; }
        public string AcquirerTransactionStatusURL { get; set; }
    }

    public static class IDealConnectorOptionsExtensions
    {
        public static IDealConnectorOptions WithSubId(this IDealConnectorOptions options, string subId)
        {
            return new IDealConnectorOptions
            {
                ClientCertificate = options.ClientCertificate,
                ClientCertificatePassword = options.ClientCertificatePassword,
                AcquirerCertificate = options.AcquirerCertificate,
                AcquirerTimeout = options.AcquirerTimeout,
                MerchantId = options.MerchantId,
                SubId = subId,
                ExpirationPeriod = options.ExpirationPeriod,
                MerchantReturnURL = options.MerchantReturnURL,
                AcquirerURL = options.AcquirerURL,
                AcquirerDirectoryURL = options.AcquirerDirectoryURL,
                AcquirerTransactionURL = options.AcquirerTransactionURL,
                AcquirerTransactionStatusURL = options.AcquirerTransactionStatusURL,
            };
        }
    }
}
