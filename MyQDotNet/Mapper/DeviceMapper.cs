using System;
using MyQDotNet.Devices;
using MyQDotNet.Responses;

namespace MyQDotNet.Mapper
{
    public class DeviceMapper
    {
        public static Device MapResponse(ItemResponse response, MyQ myQ)
        {
            // -- Device Types
            // wifigdogateway
            // wifigaragedooropener
            
            // -- Device Families
            // gateway
            // garagedoor
            
            switch (response.DeviceFamily)
            {
                case "gateway": 
                    var gateway = new Gateway();
                    gateway = (Gateway) SetBaseFields(gateway, response);
                    gateway.State = new GatewayState
                    {
                        FirmwareVersion = response.State.FirmwareVersion,
                        HomekitCapable = response.State.HomekitCapable,
                        HomekitEnabled = response.State.HomekitEnabled,
                        Learn = response.State.Learn,
                        LearnMode = response.State.LearnMode,
                        UpdatedDate = response.State.UpdatedDate,
                        PhysicalDevices = response.State.PhysicalDevices,
                        PendingBootloadAbandoned = response.State.PendingBootloadAbandoned,
                        LastStatus = response.State.LastStatus ?? default
                    };
                    return gateway;
                case "garagedoor":
                    var door = new GarageDoor(myQ);
                    door = (GarageDoor) SetBaseFields(door, response);
                    door.ParentDevice = response.ParentDevice;
                    door.ParentDeviceId = response.ParentDeviceId;
                    door.State = new GarageDoorState
                    {
                        GdoLockConnected = response.State.GdoLockConnected,
                        AttachedWorkLightErrorPresent = response.State.AttachedWorkLightErrorPresent,
                        DoorState = GetDoorState(response.State.DoorState),
                        Open = response.State.Open,
                        Close = response.State.Close,
                        LastUpdate = response.State.LastUpdate ?? default,
                        PassthroughInterval = response.State.PassthroughInterval,
                        DoorAjarInterval = response.State.DoorAjarInterval,
                        InvalidCredentialWindow = response.State.InvalidCredentialWindow,
                        InvalidShutoutPeriod = response.State.InvalidShutoutPeriod,
                        IsUnattendedOpenAllowed = response.State.IsUnattendedOpenAllowed,
                        IsUnattendedCloseAllowed = response.State.IsUnattendedCloseAllowed,
                        AuxRelayDelay = response.State.AuxRelayDelay,
                        UseAuxRelay = response.State.UseAuxRelay,
                        RexFiresDoor = response.State.RexFiresDoor,
                        CommandChannelReportStatus = response.State.CommandChannelReportStatus,
                        ControlFromBrowser = response.State.ControlFromBrowser,
                        ReportForced = response.State.ReportForced,
                        ReportAjar = response.State.ReportAjar,
                        MaxInvalidAttempts = response.State.MaxInvalidAttempts ?? default,
                        Online = response.State.Online,
                        LastStatus = response.State.LastStatus ?? default
                    };
                    return door;
                default:
                    throw new InvalidOperationException();
            }
        }

        private static DoorState GetDoorState(string doorState)
        {
            if (!Enum.TryParse<DoorState>(doorState, true, out var parsed))
                throw new InvalidOperationException();

            return parsed;
        }
        
        

        private static Device SetBaseFields(Device device, ItemResponse response)
        {
            device.Href = response.Href;
            device.SerialNumber = response.SerialNumber;
            device.DeviceFamily = response.DeviceFamily;
            device.DevicePlatform = response.DevicePlatform;
            device.DeviceType = response.DeviceType;
            device.Name = response.Name;
            device.CreatedDate = response.CreatedDate;

            return device;
        }
    }
}