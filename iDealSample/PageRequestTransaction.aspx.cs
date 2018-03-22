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
using System.Globalization;

namespace ING.iDealSample
{
    /// <summary>
    /// This class represents the Request Transaction Page
    /// (sample implementation of "Betaalprotocol").
    /// </summary>
    public partial class PageRequestTransaction : System.Web.UI.Page
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
                string issuerid = Request.QueryString["IssuerId"];

                if (!Utility.IsValidInt(issuerid))
                {
                    LabelErrorValue.Text = String.Format(Properties.Resources.IllegalNumber, "Issuerid");
                }
                TextBoxIssuerIdValue.Text = issuerid;

                LabelMerchantIdValue.Text = HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["MerchantId"]);
                LabelSubIdValue.Text = HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["SubId"]);

                TextBoxExpirationPeriodValue.Text = HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["ExpirationPeriod"]);
                TextBoxMerchantReturnUrlValue.Text = HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["MerchantReturnUrl"]);
            }
        }


        /// <summary>
        /// Create a <see cref="Transaction"/> and send it to the acquirer.
        /// </summary>
        /// <returns>True if successful, false if an exception occurs.</returns>
        private bool RequestTransaction()
        {
            try
            {
                Transaction transaction = new Transaction();
                decimal amount = 0;

                try
                {
                    amount = Decimal.Parse(TextBoxAmountValue.Text, new CultureInfo("en-US"));
                }
                catch (FormatException)
                {
                    LabelErrorValue.Text = String.Format(Properties.Resources.IllegalNumber, "Amount");
                    return false;
                }

                transaction.Amount = amount;
                transaction.Description = TextBoxDescriptionValue.Text;
                transaction.PurchaseId = TextBoxPurchaseIdValue.Text;
                transaction.IssuerId = TextBoxIssuerIdValue.Text;
                transaction.EntranceCode = TextBoxEntranceCodeValue.Text;

                Connector connector = new Connector();
                // Override MerchantId loaded from configuration
                //connector.MerchantId = "025152899";
                connector.ExpirationPeriod = HttpUtility.HtmlEncode(TextBoxExpirationPeriodValue.Text);
                connector.MerchantReturnUrl = new Uri(TextBoxMerchantReturnUrlValue.Text);

                transaction = connector.RequestTransaction(transaction);
                LabelTransactionIdValue.Text = HttpUtility.HtmlEncode(transaction.Id);
                LabelIssuerAuthenticationUrlValue.Text = HttpUtility.HtmlDecode(transaction.IssuerAuthenticationUrl.ToString());
                LabelAcquirerIdValue.Text = HttpUtility.HtmlEncode(transaction.AcquirerId);

                return true;
            }
            catch (IDealException ex)
            {
                LabelErrorValue.Text = ex.ErrorRes.Error.consumerMessage;
                return false;
            }
        }


        /// <summary>
        /// Request Transaction button event handler.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e"><see cref="EventArgs"/> containing arguments for the event.</param>
        protected void ButtonRequestTransaction_Click(object sender, EventArgs e)
        {
            if (RequestTransaction())
            {
                ButtonIssuerAuthentication.Enabled = true;
            }
        }

        /// <summary>
        /// Issuer Authentication button event handler.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e"><see cref="EventArgs"/> containing arguments for the event.</param>
        protected void ButtonIssuerAuthentication_Click(object sender, EventArgs e)
        {
            Response.Redirect(LabelIssuerAuthenticationUrlValue.Text);
        }
    }
}
