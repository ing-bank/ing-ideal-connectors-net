using System;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace iDealAdvancedConnector
{    
    /// <summary>
    /// This class holds the merchant configuration.
    /// </summary>
    internal class MerchantConfig : ICloneable
    {
        /// <summary>
        /// The MerchantID
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// The Merchant SubId
        /// </summary>
        public string SubId { get; set; }

        /// <summary>
        /// The merchant return url
        /// </summary>
        public Uri merchantReturnUrl;

        /// <summary>
        /// The merchant certificate
        /// </summary>
        public X509Certificate2 ClientCertificate { get; set; }

        /// <summary>
        /// The expiration period
        /// </summary>
        public string ExpirationPeriod { get; set; }

        /// <summary>
        /// The acquirer certificate
        /// </summary>
        public X509Certificate2 aquirerCertificate;

        /// <summary>
        /// The acquirer url.
        /// </summary>
        public Uri acquirerURL;

        /// <summary>
        /// The acquirer directory url. Used for iTT simulation.
        /// </summary>
        public Uri acquirerUrlDIR;

        /// <summary>
        /// The acquirer transaction url. Used for iTT simulation.
        /// </summary>
        public Uri acquirerUrlTRA;

        /// <summary>
        /// The acquirer transaction status url. Used for iTT simulation.
        /// </summary>
        public Uri acquirerUrlSTA;

        /// <summary>
        /// The acquirer timeout
        /// </summary>
        public int acquirerTimeout;

        /// <summary>
        /// The currency
        /// </summary>
        public string currency;

        /// <summary>
        /// The language
        /// </summary>
        public string language;

        /// <summary>
        /// Clones the current object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            MerchantConfig merchantConfig = new MerchantConfig
            {
                MerchantId = this.MerchantId,
                SubId = this.SubId,
                merchantReturnUrl = this.merchantReturnUrl,
                ClientCertificate = this.ClientCertificate,
                aquirerCertificate = this.aquirerCertificate,
                acquirerURL = this.acquirerURL,
                acquirerUrlDIR = this.acquirerUrlDIR,
                acquirerUrlTRA = this.acquirerUrlTRA,
                acquirerUrlSTA = this.acquirerUrlSTA,
                acquirerTimeout = this.acquirerTimeout,
                ExpirationPeriod = this.ExpirationPeriod,
                currency = this.currency,
                language = this.language
            };

            return merchantConfig;
        }

        /// <summary>
        /// Gets the default merchant config.
        /// </summary>
        /// <exception cref="InvalidCastException">Configuration setting has invalid format.</exception>
        /// <exception cref="ConfigurationErrorsException">Configuration setting is missing.</exception>
        /// <exception cref="CryptographicException">Error getting certificate from the store.</exception>
        /// <exception cref="UriFormatException">Url is not in correct format.</exception>
        public static MerchantConfig DefaultMerchantConfig(X509Certificate2 acquirerCertificate = null, X509Certificate2 clientCertificate = null,
            string merchantId = null, string merchantSubId = null, string acquirerUrl = null)
        {          
            if (Connector.defaultMerchantConfig == null)
            {
                MerchantConfig newMerchant = new MerchantConfig
                {
                    MerchantId = merchantId ?? GetAppSetting("MerchantID"),
                    SubId = merchantSubId ?? GetAppSetting("SubID")
                };
            
                String merchantReturnUrl = GetAppSetting("MerchantReturnURL");
                if (!Uri.TryCreate(merchantReturnUrl, UriKind.Absolute, out newMerchant.merchantReturnUrl))
                    throw new UriFormatException("MerchantReturnURL is not in correct format.");

                newMerchant.ClientCertificate = clientCertificate ?? GetCertificate(GetAppSetting("Privatecert"));
                newMerchant.aquirerCertificate = acquirerCertificate ?? GetCertificate(GetAppSetting("Acquirercert"));


#if false
                var acquirerUrlConfig = acquirerUrl ?? ConfigurationManager.AppSettings["AcquirerURL"];
                var acquirerDirectoryUrl = ConfigurationManager.AppSettings["AcquirerDirectoryURL"];
                var acquirerTransactionUrl = ConfigurationManager.AppSettings["AcquirerTransactionURL"];
                var acquirerTransactionStatusUrl = ConfigurationManager.AppSettings["AcquirerTransactionStatusURL"];
#endif
                //TODO port me
                var acquirerUrlConfig = acquirerUrl ?? string.Empty;
                var acquirerDirectoryUrl = string.Empty;
                var acquirerTransactionUrl = string.Empty;
                var acquirerTransactionStatusUrl = string.Empty;

                if (!String.IsNullOrEmpty(acquirerUrlConfig))
                {
                    //Check to see if other urls are given.

                    if (!String.IsNullOrEmpty(acquirerDirectoryUrl) ||
                        !String.IsNullOrEmpty(acquirerTransactionUrl) ||
                        !String.IsNullOrEmpty(acquirerTransactionStatusUrl))
                    {
                        throw new NotSupportedException("When acquirerURL is given then other URLs should not be supplied");
                    }

                    //We have acquirerURL. Use this url for all innner urls.
                    if (!Uri.TryCreate(acquirerUrlConfig, UriKind.Absolute, out newMerchant.acquirerURL)) throw new UriFormatException("AcquirerURL is not in correct format.");

                    newMerchant.acquirerUrlDIR = newMerchant.acquirerURL;
                    newMerchant.acquirerUrlTRA = newMerchant.acquirerURL;
                    newMerchant.acquirerUrlSTA = newMerchant.acquirerURL;
                }
                else
                {
                    //Acquirer URL is not supplied. Try to get specific acquirer URLs
                    if (!Uri.TryCreate(acquirerDirectoryUrl, UriKind.Absolute, out newMerchant.acquirerUrlDIR)) throw new UriFormatException("AcquirerDirectoryURL is not in correct format.");
                    if (!Uri.TryCreate(acquirerTransactionUrl, UriKind.Absolute, out newMerchant.acquirerUrlTRA)) throw new UriFormatException("AcquirerTransactionURL is not in correct format.");
                    if (!Uri.TryCreate(acquirerTransactionStatusUrl, UriKind.Absolute, out newMerchant.acquirerUrlSTA)) throw new UriFormatException("AcquirerTransactionStatusURL is not in correct format.");
                }
                
                string acquirerTimeout = GetAppSetting("AcquirerTimeout");
                if (!Int32.TryParse(acquirerTimeout, out newMerchant.acquirerTimeout))
                    throw new InvalidCastException("AcquirerTimeout is not in correct format.");

                newMerchant.ExpirationPeriod = GetOptionalAppSetting("ExpirationPeriod", null);

                newMerchant.currency = "EUR";
                newMerchant.language = "nl";

                XmlSignature.XmlSignature.RegisterSignatureAlghorighm();

                Connector.defaultMerchantConfig = newMerchant;
            }

            return Connector.defaultMerchantConfig;           
        }

        /// <summary>
        /// Gets an X509 certificate.
        /// </summary>
        /// <param name="subjectOrThumbprint">Subject or Thumbprint string for the certificate to get.</param>
        /// <returns><see cref="X509Certificate2"/>.</returns>
        /// <exception cref="CryptographicException">Error getting certificate from the store.</exception>
        /// <exception cref="ConfigurationErrorsException">Number of certificates found is not exactly one.</exception>
        private static X509Certificate2 GetCertificate(string subjectOrThumbprint)
        {
            return null;
            //TODO port me 
            #if false 
            WindowsImpersonationContext context = null;

            try
            {
                // If the website is using impersonation use the configured Application Pool
                // account to access the certificate store.
                WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
                if (windowsIdentity != null)
                {
                    TokenImpersonationLevel impersonationLevel = windowsIdentity.ImpersonationLevel;

                    if (impersonationLevel == TokenImpersonationLevel.Delegation || impersonationLevel == TokenImpersonationLevel.Impersonation)
                    {
                        context = WindowsIdentity.Impersonate(IntPtr.Zero);
                    }
                }                

                X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                // Change: Product Backlog Item 10247: .NET Connector - support loading certificate by Thumbprint
                // By default find certificates using SubjectName
                X509FindType findType = X509FindType.FindBySubjectName;

                // Check to see if finding certificates by Thumbprint is activated in the configuration settings
                var findCertificatesByThumbprint = GetOptionalAppSetting("FindCertificatesByThumbprint", "False");                                
                if (findCertificatesByThumbprint.ToLowerInvariant().Equals("true"))
                    findType = X509FindType.FindByThumbprint;
                
                X509Certificate2Collection certs = store.Certificates.Find(findType, subjectOrThumbprint, false);
                if (certs.Count != 1)
                {
                    string errMsg = Format("Found {0} certificates by subject/thumbprint {1}, expected 1.", certs.Count, subjectOrThumbprint);
                    if (traceSwitch.TraceError) TraceLine(errMsg);
                    throw new ConfigurationErrorsException(errMsg);
                }
                store.Close();

                return certs[0];
            }
            finally
            {
                if (context != null)
                    context.Undo();
            }
            #endif
        }

        /// <summary>
        /// Gets a value from the AppSettings.
        /// </summary>
        /// <param name="key">Key for the value to get.</param>
        /// <returns>Value as a string.</returns>
        /// <exception cref="ConfigurationErrorsException">Key does not exist.</exception>
        private static string GetAppSetting(string key)
        {
            //TODO port me 
            #if false
            string value = ConfigurationManager.AppSettings[key];

            if (String.IsNullOrEmpty(value))
                throw new ConfigurationErrorsException(Format("Cannot find key in appSettings for \"{0}\", check your web.config file.", key));

            return value;
            #endif
            return string.Empty;
        }

        /// <summary>
        /// Gets an optional value from the AppSettings.
        /// </summary>
        /// <param name="key">Key for the value to get.</param>
        /// <param name="defaultValue">Value to use if key is not available.</param>
        /// <returns>Value as a string.</returns>
        public static string GetOptionalAppSetting(string key, string defaultValue)
        {
            //string value = ConfigurationManager.AppSettings[key];
            //TODO port me 
            string value = string.Empty;

            if (String.IsNullOrEmpty(value))
                return defaultValue;

            return value;
        }
    }
}
