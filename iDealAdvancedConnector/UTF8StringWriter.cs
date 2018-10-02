using System.IO;
using System.Text;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace iDealAdvancedConnector
{
    /// <summary>
    /// UTF8StringWriter class.
    /// </summary>
    internal class UTF8StringWriter : StringWriter
    {
        /// <summary>
        /// Overrides the encoding used.
        /// </summary>
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
