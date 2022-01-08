using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using SpaceX.API.Device;

namespace NiceDishy
{
    public class DishyService
    {
        public static DishyService Shared = new DishyService();
        public async void GetStatusAsync()
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

                ApiManager.Shared.PushData(payload.toNiceDishyPayload());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            channel.ShutdownAsync().Wait();
        }

    }
}
