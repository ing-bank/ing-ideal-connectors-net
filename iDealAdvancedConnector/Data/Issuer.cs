

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace iDealAdvancedConnector.Data
{
    /// <summary>
    /// This class represents an iDeal issuer.
    /// </summary>
    public class Issuer
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="id">Issuer Id.</param>
        /// <param name="name">Issuer Name.</param>
        public Issuer(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; }
    }
}
