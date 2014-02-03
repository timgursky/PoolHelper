using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BasePoolAdapter;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace PoolAdapters
{
    public class HashFasterPoolAdapter : PoolAdapter
    {
        private const string DashboardUri = @"https://ltc.hashfaster.com/index.php?page=dashboard";
        private const string LoginUri = @"https://ltc.hashfaster.com/index.php?page=login";
        private readonly CookieContainer _cookieContainer;
        private readonly HttpClientHandler _clientHandler;
        private readonly HttpClient _httpClient;

        public HashFasterPoolAdapterOptions Options { get; set; }
        public HashFasterPoolAdapter(HashFasterPoolAdapterOptions options)
        {
            PoolName = "HashFaster";
            PoolUrl = @"http://ltc.hashfaster.com/";

            _cookieContainer = new CookieContainer();
            _clientHandler = new HttpClientHandler { CookieContainer = _cookieContainer };
            _httpClient = new HttpClient(_clientHandler);

            Options = options;
        }

        public override void GetPoolStatsAsync()
        {
            PopulateSessionCookieIfNeeded();

            var htmlNode = GetHtmlNode(GetDashboardHtmlBody());

            var poolStats = new PoolStats
            {
                GatheredOn = DateTime.Now,
                ConfirmedBalance = Decimal.Parse(GetElementContent(htmlNode, "#b-confirmed")),
                Round = new PoolRound
                {
                    Estimate = Decimal.Parse(GetElementContent(htmlNode, "#b-payout")),
                    Shares = int.Parse(GetElementContent(htmlNode, "#b-yvalid"))
                }
            };

            //TODO: Add support for workers

            InvokeGetPoolStatsCompletedEvent(poolStats);
        }

        private static string GetElementContent(HtmlNode htmlNode, string cssSelector)
        {
            var matchingNode = htmlNode.QuerySelectorAll(cssSelector).FirstOrDefault();

            return matchingNode != null ? matchingNode.InnerText : string.Empty;
        }

        private static HtmlNode GetHtmlNode(string htmlBody)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlBody);

            var node = htmlDocument.DocumentNode;

            return node;
        }

        private string GetDashboardHtmlBody()
        {
            var task = Task.Run(() => _httpClient.GetAsync(DashboardUri));
            while (!task.IsCompleted) { }
            var response = task.Result;

            response.EnsureSuccessStatusCode();

            var contentTask = Task.Run(() => response.Content.ReadAsStringAsync());
            if (!contentTask.IsCompleted) { }
            var body = contentTask.Result;

            return body;
        }

        private HttpContent AssembleLoginFormData()
        {
            var formData = new Dictionary<string, string>(2);

            formData.Add("username", Options.Credentials.UserName);
            formData.Add("password", Options.Credentials.Password);

            return new FormUrlEncodedContent(formData);
        }

        private void PopulateSessionCookieIfNeeded()
        {
            var sessionCookie = _cookieContainer.GetCookies(new Uri(DashboardUri))["PHPSESSID"];

            if (sessionCookie == null || sessionCookie.Expired)
            {
                PopulateSessionCookie();
            }
        }

        private void PopulateSessionCookie()
        {
            var client = new HttpClient(_clientHandler);

            client.Timeout = new TimeSpan(0, 1, 0);
            client.BaseAddress = new Uri(LoginUri);

            var task = Task.Run(() => client.PostAsync(LoginUri, AssembleLoginFormData()));

            while (!task.IsCompleted) { }

            var response = task.Result;

            //response.EnsureSuccessStatusCode();

            var cookieCollection = new Dictionary<string, Cookie>();

            foreach (Cookie cookie in _cookieContainer.GetCookies(new Uri(DashboardUri)))
            {
                cookieCollection.Add(cookie.Name, cookie);
            }
        }
    }
}
