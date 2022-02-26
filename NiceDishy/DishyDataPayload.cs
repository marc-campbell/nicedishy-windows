using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceDishy
{
    class DishyDataPayload
    {
        public DishyDataPayloadStatus status;
        public DateTime when;

        public DishyDataPayload(SpaceX.API.Device.Response deviceResponse)
        {
            status = new DishyDataPayloadStatus(deviceResponse);
            when = DateTime.UtcNow;

        }

        public string ToNiceDishyPayload()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    class DishySpeedTestPayload
    {
        public class DishySpeedTest
        {
            public double download;
            public double upload;
        }

        public DishySpeedTest speed;
        public DateTime when;

        public DishySpeedTestPayload(double ds, double us = 0)
        {
            speed = new DishySpeedTest();
            speed.download = ds;
            speed.upload = us;
            when = DateTime.UtcNow;
        }

        public string ToNiceDishyPayload()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
