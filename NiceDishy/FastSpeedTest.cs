using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using Newtonsoft.Json.Linq;

namespace NiceDishy
{
    class FastSpeedTest
    {
        public int fastServerCount = 1;
        public int timeout = 15;

        int[] payloadSizes = { 2048, 26214400 };
        string token;
        List<string> targetURLs;
        List<WebRequester> requesters;
        DateTime timestamp;
        DispatcherTimer timer;

        public delegate void Completed(double sp);
        public event Completed completedHandler;

        HttpClient httpClient;

        public FastSpeedTest()
        {
            httpClient = new HttpClient();
            requesters = new List<WebRequester>();
            targetURLs = new List<string>();
        }

        private async Task<bool> FetchTokenAsync()
        {
            try
            {
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri("https://fast.com");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Unable to connect to fast.com");
                    return false;
                }
                var res = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(res))
                {
                    Console.WriteLine("Unable to load fast.com");
                    return false;
                }
                //Console.Write(res);
                var matched = Matches("<script src=\"/app-a\\d{5}.js", res);
                if (matched.Count < 1)
                    return false;
                var matchedComponents = matched[0].Split('/');

                //fetch js
                var jsRequest = new HttpRequestMessage();
                jsRequest.Method = HttpMethod.Get;
                jsRequest.RequestUri = new Uri("https://fast.com/" + matchedComponents[1]);
                var jsResponse = await httpClient.SendAsync(jsRequest);
                if (!jsResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Unable to fetch js from fast.com");
                    return false;
                }
                var jsRes = await jsResponse.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(jsRes))
                {
                    Console.WriteLine("Unable to load js from fast.com");
                    return false;
                }
                //Console.Write(jsRes);
                var jsMatched = Matches("token:\"[A-Z]+", jsRes);
                if (jsMatched.Count < 1)
                {
                    Console.WriteLine("Unable to find string token.");
                    return false;
                }
                token = jsMatched[0].Split('\"')[1];

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }
        private async Task FetchTargetsAsync()
        {
            targetURLs.Clear();

            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("No token for fetching targets");
                    return;
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri("https://api.fast.com/netflix/speedtest/v2?https=true&token=" + token + "&urlCount=" + fastServerCount);
                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Unable to fetch targets from fast.com");
                    return;
                }
                var res = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(res))
                {
                    Console.WriteLine("Unable to load targets from fast.com");
                    return;
                }
                //Console.Write(res);

                var json = JObject.Parse(res);
                var targets = (JArray)json["targets"];
                foreach (JObject t in targets)
                {
                    targetURLs.Add((string)t["url"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Download()
        {
            MakeRequest(true);
        }
        public void Upload()
        {
            MakeRequest(false);
        }
        public async void MakeRequest(bool isDownload)
        {
            if (string.IsNullOrEmpty(token))
            {
                if (!await FetchTokenAsync())
                {
                    return;
                }
            }

            if (targetURLs.Count < 1)
            {
                await FetchTargetsAsync();
            }

            if (targetURLs.Count < 1)
            {
                completedHandler(0);
                return;
            }

            try
            {
                int idx = 0;
                foreach (string url in targetURLs)
                {
                    // Random rnd = new Random();
                    int numReq = isDownload ? 12 : 4;// rnd.Next(5, 10);
                    for (int i = 0; i < numReq; i++)
                    {
                        int size = payloadSizes[0];
                        if (i % (isDownload ? 5 : 2) == 0)
                            size = payloadSizes[1];
                        Uri chunkUri = new Uri(url);
                        string scheme = chunkUri.Scheme;
                        string host = chunkUri.Host;
                        string path = chunkUri.AbsolutePath + "/range/0-" + size;
                        string query = chunkUri.Query;
                        string chunkUrl = scheme + "://" + host + path + query;
                        WebRequester req = new WebRequester(
                            isDownload ? WebRequester.ReqType.download : WebRequester.ReqType.upload,
                            size,
                            chunkUrl);
                        req.reqIndex = idx++;
                        req.Start();
                        requesters.Add(req);
                    }
                }

                timestamp = DateTime.Now;
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Tick += new EventHandler((sender, e) =>
                {
                    double curSpeed = speed;
                    Console.WriteLine("Speed: {0}, {1} kbps", (int)curSpeed, (int)curSpeed / 1024);
                    int elasped = (int)(DateTime.Now - timestamp).TotalSeconds;
                    if (elasped > timeout)
                    {
                        timer.Stop();
                        CancelRequests();
                        completedHandler(curSpeed);
                    }
                });
                timer.Start();
            }
            catch
            {

            }

            return;
        }
        void CancelRequests()
        {
            foreach (WebRequester req in requesters)
            {
                if (!req.isCompleted)
                    req.Stop();
            }
        }

        public double speed
        {
            get
            {
                if (requesters.Count < 1)
                    return 0;

                double sum = 0;
                foreach (WebRequester req in requesters)
                {
                    sum += req.averageSpeed;
                }

                return sum /(double)requesters.Count;
            }
        }

        private List<string> Matches(string pattern, string text)
        {
            List<string> matchList = new List<string>();
            try
            {
                Match match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
                while (match.Success)
                {
                    matchList.Add(match.Value);
                    match = match.NextMatch();
                }
            }
            catch (Exception)
            {

            }

            return matchList;
        }
    }
}
