using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ING.iDealAdvanced.Security;
using System.Diagnostics;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace ING.iDealAdvanced.XmlSignature
{
    /// <summary>
    /// XmlSignature class used to sign an xml document
    /// </summary>
    public class XmlSignature
    {
        /// <summary>
        /// The iDeal implemented algorithm namespace
        /// </summary>
        public const string XmlDigSignRSASHA256Namespace = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

        /// <summary>
        /// Registers the signing algorithm
        /// </summary>
        public static void RegisterSignatureAlghorighm()
        {
            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), XmlDigSignRSASHA256Namespace);
        }

        /// <summary>
        /// Regex used for the sign algorithm
        /// </summary>
        private static Regex[] AlgorithmsRegex = { 
                                    new Regex("<CanonicalizationMethod[^\\\">]+\"http://www\\.w3\\.org/2001/10/xml-exc-c14n#\"", RegexOptions.Compiled),
                                    new Regex("<SignatureMethod[^\\\">]+\"http://www\\.w3\\.org/2001/04/xmldsig-more#rsa-sha256\"", RegexOptions.Compiled),
                                    new Regex("<Transform[^\\\">]+\"http://www\\.w3\\.org/2000/09/xmldsig#enveloped-signature\"", RegexOptions.Compiled),
                                    new Regex("<DigestMethod[^\\\">]+\"http://www\\.w3\\.org/2001/04/xmlenc#sha256\"", RegexOptions.Compiled),
                                    };


        /// <summary>
        /// Signs the specified xmldocument with the given key and the finderprint.
        /// </summary>
        /// <param name="doc">The XMl representation of a request to iDeal</param>
        /// <param name="key">The key used to sign</param>
        /// <param name="fingerprint">The fingerprint</param>
        /// <returns>Returns the modified xml with the signature</returns>
        public static XmlElement Sign(ref XmlDocument doc, RSA key, byte[] fingerprint)
        {
            return Sign(ref doc, key, BitConverter.ToString(fingerprint));
        }

        /// <summary>
        /// Signs the specified xmldocument with the given key and the finderprint.
        /// </summary>
        /// <param name="doc">The XMl representation of a request to iDeal</param>
        /// <param name="key">The key used to sign</param>
        /// <param name="fingerprintHex">The fingerprint</param>
        /// <returns>Returns the modified xml with the signature</returns>
        public static XmlElement Sign(ref XmlDocument doc, RSA key, string fingerprintHex)
        {
            doc.PreserveWhitespace = true;
            Trace.WriteLine("Message to sign:" + doc.OuterXml);
            SignedXml signedXml = new SignedXml(doc);

            signedXml.SigningKey = key;

            // Get the signature object from the SignedXml object.
            Signature signatureRef = signedXml.Signature;

            //Pass "" to specify that all of the current XML document should be signed.
            Reference reference = new Reference("");
            reference.DigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the Reference object to the Signature object.
            signatureRef.SignedInfo.AddReference(reference);

            signatureRef.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
            signatureRef.SignedInfo.SignatureMethod = XmlDigSignRSASHA256Namespace;

            // Add an RSAKeyValue KeyInfo
            KeyInfo keyInfo = new KeyInfo();
            KeyInfoClause clause = new RSAKeyValue(key);
            keyInfo.AddClause(clause);
            signatureRef.KeyInfo = keyInfo;

            signedXml.ComputeSignature();
            XmlElement xmlSignature = signedXml.GetXml();

            // Append the element to the XML document.
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlSignature, true));

            string xml = doc.OuterXml;

            //replace KeyValue with KeyName containing the fingerprint of the signing certificate
            string keyNameTag = "<KeyName>" + fingerprintHex.Replace("-", "") + "</KeyName>";
            xml = System.Text.RegularExpressions.Regex.Replace(xml, "<KeyValue>.*</KeyValue>", keyNameTag);

            //reload the xml document from customized xml source
            doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(xml);
            //}
            return (XmlElement)doc.DocumentElement.ChildNodes[doc.DocumentElement.ChildNodes.Count - 1];
        }


        /// <summary>
        /// Checks the signature
        /// </summary>
        /// <param name="xml">The xml representation as string</param>
        /// <param name="key">The RSA key to be used</param>
        /// <returns>True if it is signed with the given key</returns>
        internal static bool CheckSignature(string xml, RSA key)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.LoadXml(xml);

            return CheckSignature(xmlDocument, key);
        }

        /// <summary>
        /// Checks the signature
        /// </summary>
        /// <param name="xmlDocument">The xml document to sign</param>
        /// <param name="key">The key to be used</param>
        /// <returns>True if it is signed with the given key</returns>
        internal static bool CheckSignature(XmlDocument xmlDocument, RSA key)
        {
            SignedXml signedXml = new SignedXml(xmlDocument);
            Trace.WriteLine("Message to verify:" + xmlDocument.OuterXml);

            XmlNodeList signatureNode = xmlDocument.GetElementsByTagName("Signature", GlobalConstants.xmlnsDS);

            if (signatureNode.Count == 0)
            {
                Trace.WriteLine("No signature node found...");
                return false;
            }
            XmlElement signEl = (XmlElement)signatureNode[0];

            signedXml.LoadXml(signEl);

            //todo cip namespace to verify
            //this is a hack: .net doesnt know to verify a signature if it has namespace prefix in it...
            foreach (XmlNode node in signEl.SelectNodes("descendant-or-self::*[namespace-uri()='" + GlobalConstants.xmlnsDS + "']"))
            {
                node.Prefix = "";
            }

            if (signedXml.CheckSignature(key))
            {
                string signature = signatureNode[0].InnerXml;
                foreach (var regex in AlgorithmsRegex)
                {
                    if (!regex.IsMatch(signature))
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        #region Extract signature values

        /// <summary>
        /// Gets the signature node from the xmldocument
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static XmlNode GetSignatureNode(XmlDocument doc)
        {
            var xmlNode = doc.SelectSingleNode("//*[local-name()='Signature']");
            if (xmlNode == null)
                throw new ArgumentException("The document is not signed");
            return xmlNode;
        }
        #endregion
    }
}
