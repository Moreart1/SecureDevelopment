using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Security.Cryptography;

namespace Seminar.Core
{
    public abstract class BaseCacheProviderException : Exception { }
    public class SerializeDataCacheProviderException : BaseCacheProviderException { }
    public class DeserializeDataCacheProviderException : BaseCacheProviderException { }

    public class ProtectCacheProviderException : BaseCacheProviderException { }
    public class UnprotectCacheProviderException : BaseCacheProviderException { }

    public class CacheProvider
    {
        static byte[] additionalEntropy = { 5, 3, 7, 1, 5 };

        public void CacheConnections(List<ConnectionString> connections)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                using MemoryStream memoryStream = new MemoryStream();
                using XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xmlSerializer.Serialize(xmlTextWriter, connections);
                byte[] protectedData = Protect(memoryStream.ToArray());
                File.WriteAllBytes("data.protected", protectedData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Serialize data error.");
                throw new SerializeDataCacheProviderException();
            }
        }

        private byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception e)
            {
                Console.WriteLine("Protected error.");
                throw new ProtectCacheProviderException();
            }
        }


        public List<ConnectionString> GetConnectionsFromCache()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                byte[] protectedData = File.ReadAllBytes("data.protected");
                byte[] data = Unprotect(protectedData);
                return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Deserialize data error.");
                throw new DeserializeDataCacheProviderException();
            }
        }

        private byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unprotect error.");
                throw new UnprotectCacheProviderException();
            }
        }
    }
}
