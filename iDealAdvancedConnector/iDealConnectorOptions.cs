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
}
