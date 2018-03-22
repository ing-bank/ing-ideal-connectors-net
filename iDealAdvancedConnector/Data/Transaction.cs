using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

using ING.iDealAdvanced.Messages;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace ING.iDealAdvanced.Data
{
    /// <summary>
    /// This class represents an iDEAL transaction.
    /// </summary>
    /// <exception cref="ArgumentNullException">Input is empty or null.</exception>
    /// <exception cref="ArgumentException">Input contains characters that are not in the allowed character set.</exception>
    public class Transaction
    {
        /// <summary>
        /// Enum containing transaction statuses.
        /// </summary>
        public enum TransactionStatus
        {
            /// <summary>
            /// Open.
            /// </summary>
            Open,
            /// <summary>
            /// Completed sucessfully.
            /// </summary>
            Success,
            /// <summary>
            /// Cancelled.
            /// </summary>
            Cancelled,
            /// <summary>
            /// Expired.
            /// </summary>
            Expired,
            /// <summary>
            /// Failed.
            /// </summary>
            Failure
        }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Transaction()
        {
            this.Status = TransactionStatus.Open;
        }

        /// <summary>
        /// The transaction status
        /// </summary>
        public TransactionStatus Status { get; internal set; }

        /// <summary>
        /// The transaction id
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// The transaction amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The transaction created date timestamp
        /// </summary>
        public DateTime TransactionCreateDateTimestamp { get; internal set; }

        /// <summary>
        /// The transaction issuer id
        /// </summary>
        public string IssuerId { get; set; }

        /// <summary>
        /// The transaction issuer authentication url
        /// </summary>
        public Uri IssuerAuthenticationUrl { get; internal set; }

        /// <summary>
        /// The transaction consumer name
        /// </summary>
        public string ConsumerName { get; internal set; }

        /// <summary>
        /// The transaction consumer IBAN
        /// </summary>
        public string ConsumerIBAN { get; internal set; }

        /// <summary>
        /// The transaction consumer BIC
        /// </summary>
        public string ConsumerBIC { get; internal set; }

        /// <summary>
        /// The transaction aquirer id
        /// </summary>
        public string AcquirerId { get; internal set; }

        /// <summary>
        /// The transaction status date timestamp
        /// </summary>
        public DateTime StatusDateTimestamp { get; internal set; }

        /// <summary>
        /// The transaction fingerprint
        /// </summary>
        public string Fingerprint { get; internal set; }

        /// <summary>
        /// The transaction signature value
        /// </summary>
        public byte[] SignatureValue { get; internal set; }


        /// <summary>
        /// Gets or sets the description for the transaction.
        /// </summary>
        /// <exception cref="ArgumentNullException">Input is empty or null.</exception>
        /// <exception cref="ArgumentException">Input contains characters that are not in the allowed character set.</exception>
        public string Description
        {
            get { return description; }
            set
            {
                ValidateString("Description", value);
                description = value;
            }
        }
        private string description;

        /// <summary>
        /// Gets or sets the entrance code for the transaction.
        /// </summary>
        /// <exception cref="ArgumentNullException">Input is empty or null.</exception>
        /// <exception cref="ArgumentException">Input contains characters that are not in the allowed character set.</exception>
        public string EntranceCode
        {
            get { return entranceCode; }
            set
            {
                ValidateString("EntranceCode", value);
                entranceCode = value;
            }
        }
        private string entranceCode;

        /// <summary>
        /// Gets or sets the purchase ID for the transaction.
        /// </summary>
        /// <exception cref="ArgumentNullException">Input is empty or null.</exception>
        /// <exception cref="ArgumentException">Input contains characters that are not in the allowed character set.</exception>
        public string PurchaseId
        {
            get { return purchaseId; }
            set
            {
                ValidateString("PurchaseId", value);
                purchaseId = value;
            }
        }
        private string purchaseId;

        /// <summary>
        /// Formats the Transaction as a string.
        /// </summary>
        /// <returns>Transaction formatted as a string.</returns>
        public override string ToString()
        {
            string result = String.Empty;
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                string propValue = prop.GetValue(this, null) == null ? "<NULL>" : prop.GetValue(this, null).ToString();
                result += prop.Name + " = " + propValue + Environment.NewLine;
            }

            return result;
        }

        /// <summary>
        /// Validate an input string against the set of allowed characters.
        /// </summary>
        /// <param name="name">Property name that will be used in error message.</param>
        /// <param name="text">String value to check.</param>
        /// <exception cref="ArgumentException">Input contains characters that are not in the allowed character set.</exception>
        private static void ValidateString(string name, string text)
        {
            if (String.IsNullOrEmpty(text)) throw new ArgumentNullException(String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0} cannot be empty or null.", name));
            if (!Regex.IsMatch(text, Constants.stringPattern)) throw new ArgumentException(String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0} contains one or more unsupported characters.", name));
        }
    }
}
