using System.Collections.Generic;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace iDealAdvancedConnector.Data
{
    /// <summary>
    /// Country class.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        /// <param name="countryNames">The country names.</param>
        internal Country(string countryNames)
        {
            this.CountryNames = countryNames;
            this.Issuers = new List<Issuer>();
        }

        /// <summary>
        /// Gets the country names.
        /// </summary>
        public string CountryNames { get; private set; }

        /// <summary>
        /// Gets the issuers.
        /// </summary>
        public List<Issuer> Issuers { get; private set; }

        /// <summary>
        /// Adds the issuer.
        /// </summary>
        /// <param name="issuer">The issuer.</param>
        internal void AddIssuer(Issuer issuer)
        {
            if (issuer != null)
            {
                this.Issuers.Add(issuer);
            }
        }
    }
}
