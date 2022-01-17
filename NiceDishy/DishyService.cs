using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using SpaceX.API.Device;

namespace NiceDishy
{
    public class DishyService
    {
        public static DishyService Shared = new DishyService();

        public async void GetSpeedAsync()
        {
            try
            {
                var url = "https://speed.nicedishy.com/130mb";
                var client = new HttpClient();
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var now = DateTime.Now;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Download Success");
                    var res = await response.Content.ReadAsByteArrayAsync();
                    var cur = DateTime.Now;
                    var dif = cur.Subtract(now).TotalSeconds;
                    var downloadSpeed = Convert.ToDouble(res.Length) * 8 / dif;
                    var payload = new DishySpeedTestPayload(downloadSpeed);

                    ApiManager.Shared.PushSpeed(payload.ToNiceDishyPayload());
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Console.Write(res);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public async void GetDataAsync()
        {
            Channel channel = new Channel("192.168.100.1:9200", ChannelCredentials.Insecure);

            var request = new Request();
            request.GetStatus = new GetStatusRequest();

            var client = new Device.DeviceClient(channel);

            try
            {
                // var response = client.Handle(request);
                var dishyResponse = await client.HandleAsync(request);
                var payload = new DishyDataPayload(dishyResponse);

                ApiManager.Shared.PushData(payload.ToNiceDishyPayload());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            channel.ShutdownAsync().Wait();
        }

    }
}
