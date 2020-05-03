using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace MyQDotNet.Responses
{
    [PublicAPI]
    public class DevicesResponse
    {
        [JsonPropertyName("href")] public Uri Href { get; set; }

        [JsonPropertyName("count")] public long Count { get; set; }
        [JsonPropertyName("items")] public List<ItemResponse> Items { get; set; }
    }

    [PublicAPI]
    public class ItemResponse
    {
        [JsonPropertyName("href")] public Uri Href { get; set; }
        [JsonPropertyName("serial_number")] public string SerialNumber { get; set; }
        [JsonPropertyName("device_family")] public string DeviceFamily { get; set; }
        [JsonPropertyName("device_platform")] public string DevicePlatform { get; set; }
        [JsonPropertyName("device_type")] public string DeviceType { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("created_date")] public DateTimeOffset CreatedDate { get; set; }
        [JsonPropertyName("state")] public DeviceStateResponse State { get; set; }
        [JsonPropertyName("parent_device")] public Uri ParentDevice { get; set; }
        [JsonPropertyName("parent_device_id")] public string ParentDeviceId { get; set; }
    }

    [PublicAPI]
    public class DeviceStateResponse
    {
        [JsonPropertyName("online")] public bool Online { get; set; }
        [JsonPropertyName("last_status")] public DateTimeOffset? LastStatus { get; set; }
        [JsonPropertyName("updated_date")] public DateTimeOffset? UpdatedDate { get; set; }
        
        [JsonPropertyName("firmware_version")] public string FirmwareVersion { get; set; }
        [JsonPropertyName("homekit_capable")] public bool HomekitCapable { get; set; }
        [JsonPropertyName("homekit_enabled")] public bool HomekitEnabled { get; set; }
        [JsonPropertyName("learn")] public Uri Learn { get; set; }
        [JsonPropertyName("learn_mode")] public bool LearnMode { get; set; }
        [JsonPropertyName("physical_devices")] public List<string> PhysicalDevices { get; set; }

        [JsonPropertyName("pending_bootload_abandoned")]
        public bool PendingBootloadAbandoned { get; set; }
        
        [JsonPropertyName("gdo_lock_connected")] public bool GdoLockConnected { get; set; }

        [JsonPropertyName("attached_work_light_error_present")]
        public bool AttachedWorkLightErrorPresent { get; set; }

        [JsonPropertyName("door_state")] public string DoorState { get; set; }
        [JsonPropertyName("open")] public Uri Open { get; set; }
        [JsonPropertyName("close")] public Uri Close { get; set; }
        [JsonPropertyName("last_update")] public DateTimeOffset? LastUpdate { get; set; }
        [JsonPropertyName("passthrough_interval")] public string PassthroughInterval { get; set; }
        [JsonPropertyName("door_ajar_interval")] public string DoorAjarInterval { get; set; }

        [JsonPropertyName("invalid_credential_window")]
        public string InvalidCredentialWindow { get; set; }

        [JsonPropertyName("invalid_shutout_period")]
        public string InvalidShutoutPeriod { get; set; }

        [JsonPropertyName("is_unattended_open_allowed")]
        public bool IsUnattendedOpenAllowed { get; set; }

        [JsonPropertyName("is_unattended_close_allowed")]
        public bool IsUnattendedCloseAllowed { get; set; }

        [JsonPropertyName("aux_relay_delay")] public string AuxRelayDelay { get; set; }
        [JsonPropertyName("use_aux_relay")] public bool UseAuxRelay { get; set; }
        [JsonPropertyName("aux_relay_behavior")] public string AuxRelayBehavior { get; set; }
        [JsonPropertyName("rex_fires_door")] public bool RexFiresDoor { get; set; }

        [JsonPropertyName("command_channel_report_status")]
        public bool CommandChannelReportStatus { get; set; }

        [JsonPropertyName("control_from_browser")] public bool ControlFromBrowser { get; set; }
        [JsonPropertyName("report_forced")] public bool ReportForced { get; set; }
        [JsonPropertyName("report_ajar")] public bool ReportAjar { get; set; }
        [JsonPropertyName("max_invalid_attempts")] public long? MaxInvalidAttempts { get; set; }
    }
}