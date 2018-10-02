using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using iDealAdvancedConnector.Messages;

/// <summary>
/// ING.iDealAdvanced connector
/// </summary>
namespace iDealAdvancedConnector
{
    /// <summary>
    /// The serialization helper class.
    /// </summary>
    internal static class SerializationHelper
    {
        private static object thisLock = new object();

        /// <summary>
        /// Initialize the serializers table.
        /// </summary>
        /// <returns>Initializes serializers table.</returns>
        private static Hashtable InitSerializers()
        {
            if (serializers == null)
            {
                lock (thisLock)
                {
                    if (serializers == null)
                    {
                        Hashtable result = new Hashtable();

                        XmlAttributes attrs = new XmlAttributes();
                        attrs.XmlIgnore = true;

                        foreach (Type t in new Type[]{
                            typeof(DirectoryReq),
                            typeof(AcquirerTrxReq),
                            typeof(AcquirerStatusReq)})
                        {
                            XmlAttributeOverrides overrides = new XmlAttributeOverrides();
                            overrides.Add(t, "createDateTimestamp", attrs);

                            result.Add(t.Name, new XmlSerializer(t, overrides));
                        }

                        return result;
                    }
                    else return serializers;
                }
            }
            else return serializers;
        }

        private static Hashtable serializers = InitSerializers();

        /// <summary>
        /// Serializes a custom Object to XML string.
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize.</typeparam>
        /// <param name="obj">Object instance to serialize.</param>
        /// <returns>XML string.</returns>
        internal static string SerializeObject<T>(Object obj)
        {
            StringWriter sw = new UTF8StringWriter();

            XmlSerializer xs = GetSerializer<T>();

            xs.Serialize(sw, obj);

            return sw.ToString();
        }


        /// <summary>
        /// Get a <see cref="XmlSerializer"/> that can serialize objects of the given type.
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize.</typeparam>
        /// <returns>Xml Serializer for the object type.</returns>
        private static XmlSerializer GetSerializer<T>()
        {
            XmlSerializer serializer = serializers[typeof(T).Name] as XmlSerializer;

            if (serializer == null)
            {
                serializer = new XmlSerializer(typeof(T));
                serializers.Add(typeof(T).Name, serializer);
            }
            return serializer;

        }


        /// <summary>
        /// Deserializes an XML string to an Object.
        /// </summary>
        /// <typeparam name="T">Type of the object to deserialize.</typeparam>
        /// <param name="xmlString">XML string to deserialize.</param>
        /// <returns>Object created from XML string.</returns>
        internal static T DeserializeObject<T>(string xmlString)
        {
            XmlSerializer xs = GetSerializer<T>();

            return (T)xs.Deserialize(new StringReader(xmlString));
        }

    }

}
