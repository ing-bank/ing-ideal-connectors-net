

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace iDealAdvancedConnector.Messages
{
    /// <summary>
    /// Merchant interface.
    /// </summary>
    public interface IMerchant
    {
        /// <summary>
        /// Merchant ID.
        /// </summary>
        string merchantID { get; set; }
        /// <summary>
        /// Mercahnt SubID.
        /// </summary>
        string subID { get; set; }
    }
}
