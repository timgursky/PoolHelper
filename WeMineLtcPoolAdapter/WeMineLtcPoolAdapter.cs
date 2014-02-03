using System;
using System.Linq;
using System.Net;
using BasePoolAdapter;
using Newtonsoft.Json.Linq;

namespace PoolAdapters
{
    public class WeMineLtcPoolAdapter : PoolAdapter
    {
        private const string ApiEndpoint = @"https://www.wemineltc.com/api";

        public WeMineLtcPoolAdapterOptions Options { get; set; }

        public WeMineLtcPoolAdapter(WeMineLtcPoolAdapterOptions options)
        {
            PoolName = "We Mine LTC";
            PoolUrl = @"http://wemineltc.com/";
            Options = options;
        }

        public override void GetPoolStatsAsync()
        {
            var client = new WebClient();

            client.QueryString.Add("api_key", Options.ApiKey);

            var responseBuffer = client.DownloadData(ApiEndpoint);
            var parsedResponse = JObject.Parse(System.Text.Encoding.Default.GetString(responseBuffer));

            var poolStats = new PoolStats
            {
                GatheredOn = new DateTime(),
                ConfirmedBalance = parsedResponse.Value<decimal>("confirmed_rewards"),
                PaidOutAmount = parsedResponse.Value<decimal>("payout_history"),
                Round = new PoolRound
                {
                    Estimate = parsedResponse.Value<decimal>("round_estimate"),
                    Shares = parsedResponse.Value<int>("round_shares")
                }
            };

            //TODO: Add support for workers

            InvokeGetPoolStatsCompletedEvent(poolStats);
        }
    }
}
