using System.Net;
using BasePoolAdapter;

namespace PoolAdapters
{
    public class HashFasterPoolAdapterOptions : PoolAdapterOptions
    {
        public string ApiKey { get; set; }
        public int UserId { get; set; }
        public NetworkCredential Credentialses { get; set; }

        public HashFasterPoolAdapterOptions(string apiKey, int userId)
        {
            ApiKey = apiKey;
            UserId = userId;
        }

        public HashFasterPoolAdapterOptions(NetworkCredential credentials)
        {
            Credentialses = credentials;
        }
    }
}
