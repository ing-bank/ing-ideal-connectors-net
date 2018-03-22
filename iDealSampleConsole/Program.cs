using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ING.iDealAdvanced.Security;
using ING.iDealAdvanced.XmlSignature;

namespace iDealSampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Method2();
        }

        private static void Method2()
        {
            Console.WriteLine("Press enter to start the test");
            Console.ReadLine();
            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");

            // Create a new XML document.
            XmlDocument doc = new XmlDocument();
            // Format the document to ignore white spaces.
            doc.PreserveWhitespace = false;
            // Load the passed XML 
            string my_xml = "<root><test>test</test></root>";
            doc.LoadXml(my_xml);

            ING.iDealAdvanced.Connector conn = new ING.iDealAdvanced.Connector();

            X509Certificate2 cert = conn.ClientCertificate;
            RSACryptoServiceProvider key = null;// conn.GetMerchantRSACryptoServiceProvider();

            XmlSignature.Sign(ref doc, key, cert.Thumbprint);

            Console.WriteLine(doc.OuterXml);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Ended");
            Console.ReadLine();
        }
    }
}
