using BasePoolAdapter;

namespace PoolAdapters
{
    public class WeMineLtcPoolAdapterOptions : PoolAdapterOptions
    {
        public string ApiKey { get; set; }

        public WeMineLtcPoolAdapterOptions(string apiKey)
        {
            ApiKey = apiKey;
        }
    }
}
