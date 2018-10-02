using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using iDealAdvancedConnector;
using iDealAdvancedConnector.Security;
using iDealAdvancedConnector.XmlSignature;

namespace iDealSampleConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Test();
        }

        private static void Test()
        {
            Console.WriteLine("Press enter to start the test");
            Console.ReadLine();
            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");

            // Create a new XML document and format to ignore white space
            var doc = new XmlDocument { PreserveWhitespace = false };

            // Load the passed XML 
            const string xml = "<root><test>test</test></root>";
            doc.LoadXml(xml);

            var connector = new Connector();

            var certificate = connector.ClientCertificate;
            XmlSignature.Sign(ref doc, null, certificate.Thumbprint);

            Console.WriteLine(doc.OuterXml);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Ended");
            Console.ReadLine();
        }
    }
}
