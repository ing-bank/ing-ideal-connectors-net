using System;
using System.Collections.Generic;
using System.Globalization;
using iDealAdvancedConnector.Messages;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace iDealAdvancedConnector.Data
{
    /// <summary>
    /// This class contains the result of the GetIssuerList function of the Directory Protocol.
    /// </summary>
    public class Issuers
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        internal Issuers(DirectoryResDirectory directoryResDirectory)
        {
            if (directoryResDirectory != null)
            {
                this.Countries = new List<Country>();
                // Create comparer for sorting
                IssuerComparer comparer = new IssuerComparer();

                foreach (var directoryResDirectoryCountry in directoryResDirectory.Country)
                {
                    var country = new Country(directoryResDirectoryCountry.countryNames);
                    foreach (var directoryResDirectoryCountryIssuer in directoryResDirectoryCountry.Issuer)
                    {
                        country.AddIssuer(new Issuer(directoryResDirectoryCountryIssuer.issuerID, directoryResDirectoryCountryIssuer.issuerName));
                    }
                    // Sort lists alphabetically on issuer name
                    country.Issuers.Sort(comparer);
                    this.Countries.Add(country);
                }

                this.DateTimestamp = directoryResDirectory.directoryDateTimestamp;
            }
        }

        /// <summary>
        /// Countries
        /// </summary>
        public List<Country> Countries { get; private set; }

        /// <summary>
        /// DateTimestamp
        /// </summary>
        public DateTime DateTimestamp { get; private set; }


        /// <summary>
        /// Comparer for issuers
        /// </summary>
        private class IssuerComparer : Comparer<Issuer>
        {
            /// <summary>
            /// Compare two objects of type <see cref="Issuer" />.
            /// </summary>
            /// <param name="first">First Issuer.</param>
            /// <param name="second">Second Issuer.</param>
            /// <returns></returns>
            public override int Compare(Issuer first, Issuer second)
            {
                if (first == null) throw new ArgumentException("first is null");
                if (second == null) throw new ArgumentException("second is null");

                return String.Compare(first.Name, second.Name, true, CultureInfo.InvariantCulture);
            }
        }
    }
}
