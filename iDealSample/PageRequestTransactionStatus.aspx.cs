using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ING.iDealAdvanced;
using ING.iDealAdvanced.Data;
using System.Text;

namespace ING.iDealSample
{
    /// <summary>
    /// This class represents the Request Transaction Status Page
    /// (sample implementation of "Navraagprotocol").
    /// </summary>
    public partial class PageRequestTransactionStatus : System.Web.UI.Page
    {
        /// <summary>
        /// Page load event handler.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e"><see cref="EventArgs"/> containing arguments for the event.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBoxTransactionIdValue.Text = Request.QueryString["trxid"];
            }
            LabelMerchantIdValue.Text = ConfigurationManager.AppSettings["MerchantId"];
            LabelSubIdValue.Text = ConfigurationManager.AppSettings["SubId"];
        }

        /// <summary>
        /// Requests the status for a <see cref="Transaction"/>.
        /// </summary>
        private void RequestTransactionStatus()
        {
            try
            {
                Connector connector = new Connector();
                // Override MerchantId loaded from configuration
                //connector.MerchantId = "025152899";
                Transaction transaction = connector.RequestTransactionStatus(TextBoxTransactionIdValue.Text);

                LabelAcquirerIdValue.Text = transaction.AcquirerId;
                LabelTransactionStatusValue.Text = transaction.Status.ToString();
                LabelConsumerNameValue.Text = transaction.ConsumerName;
                LabelFingerprintValue.Text = transaction.Fingerprint;
                LabelConsumerIBANValue.Text = transaction.ConsumerIBAN;
                LabelConsumerBICValue.Text = transaction.ConsumerBIC;
                LabelAmountValue.Text = transaction.Amount.ToString();
                LabelCurrencyValue.Text = transaction.Currency;

                string signatureString = ByteArrayToHexString(transaction.SignatureValue);

                // Place newlines in Hex String
                for (int i = 256; i > 0; i -= 32)
                    signatureString = signatureString.Substring(0, i) + "<br />" + signatureString.Substring(i);

                LabelSignatureValue.Text = signatureString;
            }
            catch (IDealException ex)
            {
                LabelErrorValue.Text = ex.ErrorRes.Error.consumerMessage;
            }
        }

        /// <summary>
        /// Request Transaction Status button event handler.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e"><see cref="EventArgs"/> containing arguments for the event.</param>
        protected void ButtonRequestTransactionStatus_Click(object sender, EventArgs e)
        {
            RequestTransactionStatus();
        }

        /// <summary>
        /// Gets the hexadecimal representation of an array of bytes.
        /// </summary>
        /// <param name="bytes">Array of bytes to get a hexadecimal representation for.</param>
        /// <returns>The hexadecimal representation of the byte array.</returns>
        private string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder result = new StringBuilder();

            foreach (byte b in bytes)
                result.Append(b.ToString("X2"));

            return result.ToString();
        }

    }
}
