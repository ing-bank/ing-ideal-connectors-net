namespace iDealAdvancedConnector
{
    public class IDealConnectorOptions
    {
        public string ClientCertificate { get; set; }
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
        public string UseCspMachineKeyStore { get; set; }
        public string UseCertificateWithEnhancedAESCryptoProvider { get; set; }
    }
}
