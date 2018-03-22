using System;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace ING.iDealAdvanced.Messages
{
    /// <summary>
    /// Request interface.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Creation DateTime stamp.
        /// </summary>
        DateTime createDateTimestamp { get; set; }
        /// <summary>
        /// Version.
        /// </summary>
        string version { get; set; }
    }
}
