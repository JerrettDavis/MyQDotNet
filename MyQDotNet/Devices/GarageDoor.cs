using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MyQDotNet.Requests;
using Newtonsoft.Json;

namespace MyQDotNet.Devices
{
    [PublicAPI]
    public class GarageDoor : Device
    {
        private readonly MyQ _myQ;

        public GarageDoor(MyQ myQ)
        {
            _myQ = myQ;
        }

        public Uri ParentDevice { get; set; }
        public string ParentDeviceId { get; set; }

        public bool CloseAllowed => ((GarageDoorState) State)?.IsUnattendedCloseAllowed ?? default;
        public bool OpenAllowed => ((GarageDoorState) State)?.IsUnattendedOpenAllowed ?? default;

        public DoorState DoorState
        {
            get => ((GarageDoorState) State)?.DoorState ?? default;
            private set => ((GarageDoorState) State).DoorState = value;
        }

        private Uri SetStateUri =>
            new Uri($"{MyQ.HostUri}/Accounts/{_myQ.AccountId}/Devices/{SerialNumber}/actions");

        private async Task PerformCommand(GarageDoorCommand command)
        {
            var json = JsonConvert.SerializeObject(new GarageDoorAction(command));
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _myQ.Client.PutAsync(SetStateUri, content);
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException();
        }

        public Task Open()
        {
            if (DoorState == DoorState.Open || DoorState == DoorState.Opening)
                return Task.CompletedTask;
            
            DoorState = DoorState.Opening;
            return PerformCommand(GarageDoorCommand.Open);
        }

        public Task Close()
        {
            if (DoorState == DoorState.Closed || DoorState == DoorState.Closing) 
                return Task.CompletedTask;
            
            DoorState = DoorState.Closing;
            return PerformCommand(GarageDoorCommand.Close);
        }
    }
    
    public struct GarageDoorCommand
    {
        public static readonly GarageDoorCommand Open = "open";
        public static readonly GarageDoorCommand Close = "close";

        private string Value { get; set; }

        private GarageDoorCommand(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator GarageDoorCommand(string value)
        {
            return new GarageDoorCommand(value);
        }

        public static implicit operator string(GarageDoorCommand command)
        {
            return command.Value;
        }
    }

    public enum DoorState
    {
        Closed,
        Closing,
        Open,
        Opening,
        Stopped,
        Transition,
        Unknown
    }

    [PublicAPI]
    public class GarageDoorState : DeviceState
    {
        public bool GdoLockConnected { get; set; }
        public bool AttachedWorkLightErrorPresent { get; set; }
        public DoorState DoorState { get; set; }
        public Uri Open { get; set; }
        public Uri Close { get; set; }
        public string PassthroughInterval { get; set; }
        public string DoorAjarInterval { get; set; }
        public string InvalidCredentialWindow { get; set; }
        public string InvalidShutoutPeriod { get; set; }
        public bool IsUnattendedOpenAllowed { get; set; }
        public bool IsUnattendedCloseAllowed { get; set; }
        public string AuxRelayDelay { get; set; }
        public bool UseAuxRelay { get; set; }
        public bool RexFiresDoor { get; set; }
        public bool CommandChannelReportStatus { get; set; }
        public bool ControlFromBrowser { get; set; }
        public bool ReportForced { get; set; }
        public bool ReportAjar { get; set; }
        public long MaxInvalidAttempts { get; set; }
    }
}