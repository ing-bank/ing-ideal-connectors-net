using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace ING.iDealAdvanced
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
