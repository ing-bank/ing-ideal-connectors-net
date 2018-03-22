using System;
using System.Collections.Generic;

using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace ING.iDealAdvanced.Security
{
    /// <summary>
    /// This class is used to allow all certificates.
    /// </summary>
    public class AcceptAllCertificates : System.Net.ICertificatePolicy
    {
        /// <summary>
        /// Checks the validation result
        /// </summary>
        /// <param name="servicePoint">The service point</param>
        /// <param name="certificate">The certificate</param>
        /// <param name="webRequest">The web request</param>
        /// <param name="iProblem">the iProblem</param>
        /// <returns>True if valid</returns>
        public bool CheckValidationResult(
            System.Net.ServicePoint servicePoint,
            System.Security.Cryptography.X509Certificates.X509Certificate cert,
            System.Net.WebRequest webRequest,
            int iProblem)
        {
            return true;
        }
    }

    /// <summary>
    /// This class is used to allow all certificates.
    /// </summary>
    public class AcceptSslCertificate : System.Net.ICertificatePolicy
    {
        /// <summary>
        /// The X509Certificate
        /// </summary>
        public X509Certificate Certificate { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public AcceptSslCertificate()
        {
            Certificate = null;
        }

        /// <summary>
        /// Constructor with X509Certificate
        /// </summary>
        /// <param name="certificate">The used certificate</param>
        public AcceptSslCertificate(X509Certificate certificate)
        {
            Certificate = certificate;
        }

        /// <summary>
        /// Checks the validation result
        /// </summary>
        /// <param name="servicePoint">The service point</param>
        /// <param name="certificate">The certificate</param>
        /// <param name="webRequest">The web request</param>
        /// <param name="iProblem">the iProblem</param>
        /// <returns>True if valid</returns>
        public bool CheckValidationResult(ServicePoint servicePoint, X509Certificate certificate, WebRequest webRequest, int iProblem)
        {
            return true;
        }
    }
}
