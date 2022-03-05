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

        FastSpeedTest downloadTester;
        FastSpeedTest uploadTester;

        public DishyService()
        {
            downloadTester = new FastSpeedTest();
            downloadTester.completedHandler += OnDownloadSpeedCompleted;
            uploadTester = new FastSpeedTest();
            uploadTester.completedHandler += OnUploadSpeedCompleted;
        }

        public void GetFastSpeed()
        {
            downloadTester.Download();
        }

        private void OnDownloadSpeedCompleted(double speed)
        {
            Console.WriteLine("Final Download Speed is {0} Mbps", (long)speed / (1024 * 1024));
            uploadTester.Upload();
        }
        private void OnUploadSpeedCompleted(double speed)
        {
            Console.WriteLine("Final Upload Speed is {0} Mbps", (long)speed / (1024 * 1024));
            PushSpeed();
        }
        public void PushSpeed()
        {
            var payload = new DishySpeedTestPayload(
                downloadTester.speed,
                uploadTester.speed);

            ApiManager.Shared.PushSpeed(payload.ToNiceDishyPayload());
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
