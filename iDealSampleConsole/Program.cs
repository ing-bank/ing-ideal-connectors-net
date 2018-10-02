using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Xml;
using iDealAdvancedConnector;
using iDealAdvancedConnector.Security;
using iDealAdvancedConnector.XmlSignature;
using Microsoft.Extensions.Configuration;

namespace iDealSampleConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var idealConnectorOptions = new IDealConnectorOptions();
            configuration.GetSection("IDealConnector").Bind(idealConnectorOptions);
            Test(idealConnectorOptions);
        }

        private static void Test(IDealConnectorOptions idealConnectorOptions)
        {
            Console.WriteLine("Press enter to start the test");
            Console.ReadLine();
            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");

            // Create a new XML document and format to ignore white space
            var doc = new XmlDocument { PreserveWhitespace = false };

            // Load the passed XML 
            const string xml = "<root><test>test</test></root>";
            doc.LoadXml(xml);

            var connector = new Connector(new HttpClient(), idealConnectorOptions);

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
