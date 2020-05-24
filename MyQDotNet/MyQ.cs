using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MyQDotNet.Common.Extensions;
using MyQDotNet.Devices;
using MyQDotNet.Mapper;
using MyQDotNet.Requests;
using MyQDotNet.Responses;
using Newtonsoft.Json;

namespace MyQDotNet
{
    [PublicAPI]
    public class MyQ
    {
        public const string AppId = "JVM/G9Nwih5BwKgNCjLxiFUQxQijAebyyg8QUHr7JOrP+tuPb8iHfRHKwTmDzHOu";
        public const string BaseApiVersion = "5";
        public const string DeviceApiVersion = "5.1";
        public const string HostUri = "https://api.myqdevice.com/api/v{0}";
        public const string GarageDoorIdentifier = "garagedoor";
        public static readonly TimeSpan StateUpdateInterval = TimeSpan.FromSeconds(5);

        public AccountInfoResponse AccountInfo { get; private set; }
        public Guid AccountId => AccountInfo.Account.Id;
        public Dictionary<string, Device> Devices { get; }
        
        public IEnumerable<GarageDoor> GarageDoors => 
            Devices.Values.Where(p => p.DeviceFamily == GarageDoorIdentifier).Cast<GarageDoor>();

        public HttpClient Client { get; }
        private DateTime? _lastStateUpdate;
        private string _securityToken;

        public MyQ(HttpClient client)
        {
            Client = client;
            Client.DefaultRequestHeaders.Add("MyQApplicationId", AppId);
            
            AccountInfo = new AccountInfoResponse();
            Devices = new Dictionary<string, Device>();

            _securityToken = "";
        }
        
        private static Uri LoginUri => new Uri(string.Format($"{HostUri}/Login", BaseApiVersion));
        public async Task<bool> Authenticate(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new AuthenticateRequest(username, password));
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(LoginUri, content);
            if (!response.IsSuccessStatusCode) return false;
            
            var body = await response.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<LoginResponse>(body);

            _securityToken = parsed.SecurityToken;
                
            Client.DefaultRequestHeaders.Add("SecurityToken", _securityToken);

            AccountInfo = await GetAccountInfo();

            await UpdateDeviceInfo();

            return true;

        }

        private static Uri AccountInfoUri => new Uri(string.Format($"{HostUri}/My", BaseApiVersion)).AddQuery("expand", "account");
        private async Task<AccountInfoResponse> GetAccountInfo()
        {
            var response = await Client.GetAsync(AccountInfoUri);
            if (!response.IsSuccessStatusCode) throw new AuthenticationException();
            
            var body = await response.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<AccountInfoResponse>(body);

            return parsed;
        }

        
        private Uri DevicesUri => new Uri(string.Format($"{HostUri}/Accounts/{AccountId}/Devices", DeviceApiVersion));
        private async Task UpdateDeviceInfo()
        {
            var callDt = DateTime.UtcNow;
            _lastStateUpdate ??= callDt - StateUpdateInterval;

            var nextAvailableCallDt = _lastStateUpdate - StateUpdateInterval;
            if (callDt < nextAvailableCallDt)
                return;
            
            var response = await Client.GetAsync(DevicesUri);
            if (!response.IsSuccessStatusCode) throw new AuthenticationException();
            
            var body = await response.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<DevicesResponse>(body);
            
            foreach (var device in parsed.Items.Select(e => DeviceMapper.MapResponse(e, this)))
            {
                var serial = device.SerialNumber;
                if (serial == null)
                    continue;

                if (Devices.ContainsKey(serial))
                    Devices[serial] = device;
                else
                    Devices.Add(serial, device);
            }
            
            _lastStateUpdate = DateTime.UtcNow;
        }
    }
}