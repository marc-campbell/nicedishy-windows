using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceDishy
{
    class DishyDataPayloadStatus
    {
        public string hardwareVersion;
        public string softwareVersion;
        public ulong uptimeSeconds;
        public int snr;
        public float downlinkThroughputBps;
        public float uplinkThroughputBps;
        public float popPingLatencyMs;
        public float popPingDropRate;
        public int secondsObstructed;
        public float percentObstructed;

        public DishyDataPayloadStatus(SpaceX.API.Device.Response deviceResponse) {
            hardwareVersion = deviceResponse.DishGetStatus.DeviceInfo.HardwareVersion;
            softwareVersion = deviceResponse.DishGetStatus.DeviceInfo.SoftwareVersion;

            uptimeSeconds = deviceResponse.DishGetStatus.DeviceState.UptimeS;

            snr = 0; // TODO

            downlinkThroughputBps = deviceResponse.DishGetStatus.DownlinkThroughputBps;
            uplinkThroughputBps = deviceResponse.DishGetStatus.UplinkThroughputBps;
            popPingLatencyMs = deviceResponse.DishGetStatus.PopPingLatencyMs;
            popPingDropRate = deviceResponse.DishGetStatus.PopPingDropRate;

            secondsObstructed = 0; // TODO
            percentObstructed = 0; // TODO
        }
    }
}
