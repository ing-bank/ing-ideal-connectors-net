using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using iDealAdvancedConnector.Messages;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace iDealAdvancedConnector.Data
{
    /// <summary>
    /// This class represents an iDEAL exception.
    /// </summary>
    [Serializable]
    public sealed class IDealException : Exception
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public IDealException() { }
        
        
        /// <summary>
        /// Constructor using an error message.
        /// </summary>
        /// <param name="message">The error message to use.</param>
        public IDealException(string message) : base(message) { }

        /// <summary>
        /// Constructor using an error message and inner exception.
        /// </summary>
        /// <param name="message">The error message to use.</param>
        /// <param name="inner">The inner exception to use.</param>
        public IDealException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Constructor using an <see cref="ErrorRes" /> object.
        /// </summary>
        /// <param name="errorRes">The <see cref="ErrorRes" /> containing the exception data.</param>
        internal IDealException(AcquirerErrorRes errorRes)
            : base(errorRes.Error.errorMessage)
        {
            this.errorRes = errorRes;
        }

        private AcquirerErrorRes errorRes;

        private IDealException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            errorRes = SerializationHelper.DeserializeObject<AcquirerErrorRes>(info.GetString("errorRes"));
        }

        /// <summary>
        /// The <see cref="ErrorRes" /> data received from iDEAL, containing the iDEAL error data..
        /// </summary>
        public AcquirerErrorRes ErrorRes
        {
            get { return errorRes; }
        }

        /// <summary>
        /// Override the GetObjectData method to serialize custom values.
        /// </summary>
        /// <param name="info">Represents the SerializationInfo of the exception.</param>
        /// <param name="context">Represents the context information of the exception.</param>
        /// <exception cref="T:System.ArgumentNullException">The info parameter is a null reference (Nothing in Visual Basic).</exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/></PermissionSet>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("errorRes", SerializationHelper.SerializeObject<AcquirerErrorRes>(errorRes));
            base.GetObjectData(info, context);
        }

    }
}
