using System;
using System.Collections.Generic;

namespace MyQDotNet
{
    public class Device
    {
        public Uri Href { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceFamily { get; set; }
        public string DevicePlatform { get; set; }
        public string DeviceType { get; set; }
        public string Name { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public virtual DeviceState State { get; set; }
        
        public bool Online => State?.Online ?? default;
    }

    public class DeviceState
    {
        public DateTimeOffset LastUpdate { get; set; }
        public DateTimeOffset LastStatus { get; set; }
        public bool Online { get; set; }
    }
}