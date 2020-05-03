using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace MyQDotNet.Devices
{
    public class Gateway : Device
    {
        public string FirmwareVersion => ((GatewayState) State)?.FirmwareVersion;
    }

    [PublicAPI]
    public class GatewayState : DeviceState
    {
        public string FirmwareVersion { get; set; }
        public bool HomekitCapable { get; set; }
        public bool HomekitEnabled { get; set; }
        public Uri Learn { get; set; }
        public bool LearnMode { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public IEnumerable<string> PhysicalDevices { get; set; }
        public bool PendingBootloadAbandoned { get; set; }
    }
}