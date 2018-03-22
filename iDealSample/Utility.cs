using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace ING.iDealSample
{
    public static class Utility
    {
        private const string stringPattern = "^[-A-Za-z0-9= %*+,./&@\"':;?()$]*$";
        private const string expirationPattern = "^[-A-Z0-9$]*$";

        public static bool IsValidExpiration(string value, bool mandatory)
        {
            if (mandatory && String.IsNullOrEmpty(value))
            {
                return false;
            }
            return Regex.IsMatch(value, expirationPattern);
        }

        public static bool IsValidString(string value, bool mandatory)
        {
            if (mandatory && String.IsNullOrEmpty(value))
            {
                return false;
            }
            return Regex.IsMatch(value, stringPattern); 
        }

        public static bool IsValidInt(string value)
        {
            int i = 0;
            return int.TryParse(value, out i);
        }

        public static bool IsValidUri(string uri)
        {
            Uri newUri;
            return Uri.TryCreate(uri,UriKind.Absolute, out newUri);
        }
    }
}
