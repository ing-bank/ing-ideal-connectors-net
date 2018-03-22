using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ING.iDealSample
{
    public class Global : System.Web.HttpApplication
    {

        /// <summary>
        /// Application start event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e"><see cref="EventArgs"/> containing event argument.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            // Remove these line if the webserver does not require a proxy to do outgoing http requests.
            ING.iDealAdvanced.XmlSignature.XmlSignature.RegisterSignatureAlghorighm();
            System.Net.ServicePointManager.CertificatePolicy = new ING.iDealAdvanced.Security.AcceptAllCertificates();

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ProxyAddress"]))
            {
                HttpWebRequest.DefaultWebProxy = new WebProxy(ConfigurationManager.AppSettings["ProxyAddress"], true);
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ProxyAddress"]) && !String.IsNullOrEmpty(ConfigurationManager.AppSettings["ProxyPassword"]))
            {
                HttpWebRequest.DefaultWebProxy.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ProxyUsername"], ConfigurationManager.AppSettings["ProxyPassword"]);
            }

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            this.Error += new EventHandler(Global_Error);
        }

        /// <summary>
        /// Error event handler.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e"><see cref="EventArgs"/> containing event argument.</param>
        private void Global_Error(object sender, EventArgs e)
        {
        }

        

        /// <summary>
        /// Application stop event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e"><see cref="EventArgs"/> containing event argument.</param>
        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
