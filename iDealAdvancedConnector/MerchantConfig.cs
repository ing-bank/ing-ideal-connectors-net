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
        public static MerchantConfig DefaultMerchantConfig(IDealConnectorOptions idealConnectorOptions)
        {          
            if (Connector.defaultMerchantConfig == null)
            {
                MerchantConfig newMerchant = new MerchantConfig
                {
                    MerchantId = idealConnectorOptions.MerchantId,
                    SubId = idealConnectorOptions.SubId
                };
            
                String merchantReturnUrl = idealConnectorOptions.MerchantReturnURL;
                if (!Uri.TryCreate(merchantReturnUrl, UriKind.Absolute, out newMerchant.merchantReturnUrl))
                    throw new UriFormatException("MerchantReturnURL is not in correct format.");

                newMerchant.ClientCertificate = GetCertificateFromBase64String(idealConnectorOptions.ClientCertificate, idealConnectorOptions.ClientCertificatePassword);
                newMerchant.aquirerCertificate = GetCertificateFromBase64String(idealConnectorOptions.AcquirerCertificate);

                var acquirerUrlConfig = idealConnectorOptions.AcquirerURL;
                var acquirerDirectoryUrl = idealConnectorOptions.AcquirerDirectoryURL;
                var acquirerTransactionUrl = idealConnectorOptions.AcquirerTransactionURL;
                var acquirerTransactionStatusUrl = idealConnectorOptions.AcquirerTransactionStatusURL;


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
                
                string acquirerTimeout = idealConnectorOptions.AcquirerTimeout;
                if (!Int32.TryParse(acquirerTimeout, out newMerchant.acquirerTimeout))
                    throw new InvalidCastException("AcquirerTimeout is not in correct format.");

                newMerchant.ExpirationPeriod = string.IsNullOrWhiteSpace(idealConnectorOptions.ExpirationPeriod) 
                        ? idealConnectorOptions.ExpirationPeriod 
                        : null;

                newMerchant.currency = "EUR";
                newMerchant.language = "nl";

                XmlSignature.XmlSignature.RegisterSignatureAlghorighm();

                Connector.defaultMerchantConfig = newMerchant;
            }

            return Connector.defaultMerchantConfig;           
        }

        static X509Certificate2 GetCertificateFromBase64String(string cert, string pwd = null)
        {
            try
            {
                byte[] rawData = Convert.FromBase64String(cert);

                if(!string.IsNullOrWhiteSpace(pwd))
                {
                    return new X509Certificate2(rawData, pwd);
                }
                else
                {
                    return new X509Certificate2(rawData);
                }
            }
            catch 
            {
                return null;
            }
        }
    }
}
