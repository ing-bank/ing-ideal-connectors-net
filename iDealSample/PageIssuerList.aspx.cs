using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ING.iDealAdvanced;
using ING.iDealAdvanced.Data;
using ING.iDealSample.Custom;

namespace ING.iDealSample
{
    /// <summary>
    /// This class represents the Issuer List Page
    /// (sample implementation of "Directoryprotocol").
    /// </summary>
    public partial class PageIssuerList : System.Web.UI.Page
    {
        /// <summary>
        /// Page load event handler.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e"><see cref="EventArgs"/> containing arguments for the event.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelAcquirerUrlValue.Text = ConfigurationManager.AppSettings["AcquirerUrl"];
            LabelMerchantIdValue.Text = ConfigurationManager.AppSettings["MerchantId"];
            LabelSubIdValue.Text = ConfigurationManager.AppSettings["SubId"];

            //DropDownListIssuers = new GroupedDropDownList();
        }


        /// <summary>
        /// Populates the list of issuers.
        /// </summary>
        private void PopulateIssuerList()
        {
            try
            {
                DropDownListIssuers.Items.Clear();

                // Show the list of issuers
                DropDownListIssuers.Items.Add(new ListItem("Kies uw bank...", "-1"));
                
                DropDownListIssuers.Items.Add(new ListItem("--Overige banken---", "-1"));
                
                foreach (var country in Issuers.Countries)
                {
                    foreach (var issuer in country.Issuers)
                    {
                        DropDownListIssuers.Items.Add(new ListItem(string.Format("{0}|{1}",issuer.Name, country.CountryNames), issuer.Id.ToString()));
                    }
                }
            }
            catch (IDealException ex)
            {
                LabelErrorValue.Text = ex.ErrorRes.Error.consumerMessage;
            }
        }


        /// <summary>
        /// Get Issuers button clicked event handler.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e"><see cref="EventArgs"/> containing arguments for the event.</param>
        protected void ButtonGetIssuerList_Click(object sender, EventArgs e)
        {
            PopulateIssuerList();

            // Enable Request Transaction button if at least 1 issuer was returned
            if (DropDownListIssuers.Items.Count > 0) ButtonTrxReq.Enabled = true;

        }

        /// <param name="sender">Event sender</param>
        /// <param name="e"><see cref="EventArgs"/> containing arguments for the event.</param>
        protected void ButtonTrxReq_Click(object sender, EventArgs e)
        {
            if (DropDownListIssuers.SelectedItem.Value != "-1")
                Response.Redirect("PageRequestTransaction.aspx?IssuerId=" + DropDownListIssuers.SelectedItem.Value);
        }

        private Issuers Issuers
        {
            get
            {
                // Try to get from cache first
                Issuers issuers = Page.Cache["Issuers"] as Issuers;

                if (issuers == null)
                {
                    // No issuer list was cached, get it from iDeal service
                    Connector connector = new Connector();
                    // Override MerchantId loaded from configuration
                    //connector.MerchantId = "025152899";

                    issuers = connector.GetIssuerList();

                    // Add the issuer list to the cache, set cache expiration to 1 day from now
                    Page.Cache.Add("Issuers", issuers, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                return issuers;
            }
        }

    }
}
