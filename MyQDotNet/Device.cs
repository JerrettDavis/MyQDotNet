using System;
using JetBrains.Annotations;

namespace MyQDotNet
{
    [PublicAPI]
    public class Device
    {
        public Uri Href { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string DeviceFamily { get; set; } = null!;
        public string DevicePlatform { get; set; } = null!;
        public string DeviceType { get; set; } = null!;
        public string Name { get; set; } = null!;

        public DateTimeOffset CreatedDate { get; set; }

        public virtual DeviceState State { get; set; } = null!;

        public bool Online => State?.Online ?? default;
    }

    [PublicAPI]
    public class DeviceState
    {
        public DateTimeOffset LastUpdate { get; set; }
        public DateTimeOffset LastStatus { get; set; }
        public bool Online { get; set; }
    }
}