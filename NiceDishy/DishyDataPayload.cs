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

        public string toNiceDishyPayload()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
