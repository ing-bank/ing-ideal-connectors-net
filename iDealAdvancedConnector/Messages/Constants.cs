using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace ING.iDealAdvanced.Messages
{
    /// <summary>
    /// iDeal Connector constants
    /// </summary>
    internal static class Constants
    {
        //2007-10-04T11:25:23.000Z
        /// <summary>
        /// The iDeal date format
        /// </summary>
        public const string iDealDateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

        /// <summary>
        /// The regex value used to check an string pattern.
        /// </summary>
        public const string stringPattern = "^[-A-Za-z0-9= %*+,./&@\"':;?()$]*$";
    }
}
