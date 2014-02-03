using System;
using System.Globalization;
using System.Net;
using BasePoolAdapter;
using Newtonsoft.Json.Linq;

namespace PoolAdapters
{
    public class HashFasterPoolAdapter : PoolAdapter
    {
        private const string ApiEndpoint = @"https://ltc.hashfaster.com/index.php";

        public HashFasterPoolAdapterOptions Options { get; set; }
        public HashFasterPoolAdapter(HashFasterPoolAdapterOptions options)
        {
            PoolName = "HashFaster";
            PoolUrl = @"http://ltc.hashfaster.com/";
            Options = options;
        }

        public override void GetPoolStatsAsync()
        {
            var client = new WebClient();

            client.QueryString.Add("api_key", Options.ApiKey);
            client.QueryString.Add("page", "api");
            client.QueryString.Add("action", "getdashboarddata");
            client.QueryString.Add("id", Options.UserId.ToString(CultureInfo.InvariantCulture));

            var responseBuffer = client.DownloadData(ApiEndpoint);
            var parsedResponse = JObject.Parse(System.Text.Encoding.Default.GetString(responseBuffer));

            var poolStats = new PoolStats
            {
                GatheredOn = new DateTime(),
                ConfirmedBalance = parsedResponse.Value<decimal>("confirmed_rewards"),
                Round = new PoolRound
                {
                    Estimate = parsedResponse.Value<decimal>("block"),
                    Shares = parsedResponse.Value<int>("valid")
                }
            };

            //TODO: Add support for workers

            InvokeGetPoolStatsCompletedEvent(poolStats);
        }
    }
}
