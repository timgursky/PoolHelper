using System;
using System.Linq;
using System.Net;
using BasePoolAdapter;
using Newtonsoft.Json.Linq;

namespace PoolAdapters
{
    public class WeMineLtcPoolAdapter : PoolAdapter
    {
        private const string ApiEndpoint = @"https://www.wemineltc.com/api?api_key=";

        public string ApiKey { get; set; }

        public WeMineLtcPoolAdapter(string apiKey)
        {
            PoolName = "We Mine LTC";
            PoolUrl = @"http://wemineltc.com/";
            ApiKey = apiKey;
        }

        public override void GetPoolStatsAsync()
        {
            var client = new WebClient();

            var responseBuffer = client.DownloadData(ApiEndpoint);
            var parsedResponse = JObject.Parse(System.Text.Encoding.Default.GetString(responseBuffer));

            var str = parsedResponse.Value<string>("last_block");

            InvokeGetPoolStatsCompletedEvent(new PoolStats());

            throw new NotImplementedException();
        }
    }
}
