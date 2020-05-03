using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using MyQDotNet.Common.Extensions;
using MyQDotNet.Mapper;
using MyQDotNet.Requests;
using MyQDotNet.Responses;
using Newtonsoft.Json;

namespace MyQDotNet
{
    public class MyQ
    {
        public const string AppId = "JVM/G9Nwih5BwKgNCjLxiFUQxQijAebyyg8QUHr7JOrP+tuPb8iHfRHKwTmDzHOu";
        public const long ApiVersion = 5;
        public static readonly string HostUri = $"https://api.myqdevice.com/api/${ApiVersion}";
        public static readonly TimeSpan StateUpdateInterval = TimeSpan.FromSeconds(5);

        public AccountInfoResponse AccountInfo { get; private set; }
        public Guid AccountId => AccountInfo.Account.Id;
        public Dictionary<string, Device> Devices { get; }

        public HttpClient Client { get; }
        private DateTime? _lastStateUpdate;
        private string _securityToken;

        public MyQ(HttpClient client)
        {
            Client = client;
            Client.DefaultRequestHeaders.Add("MyQApplicationId", AppId);
            
            AccountInfo = new AccountInfoResponse();
            Devices = new Dictionary<string, Device>();
        }

        private static Uri LoginUri => new Uri($"{HostUri}/Login");
        public async Task Authenticate(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new AuthenticateRequest(username, password));
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(LoginUri, content);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var parsed = JsonConvert.DeserializeObject<LoginResponse>(body);

                _securityToken = parsed.SecurityToken;
                
                Client.DefaultRequestHeaders.Add("SecurityToken", _securityToken);

                AccountInfo = await GetAccountInfo();

                await UpdateDeviceInfo();
            }
        }

        private static Uri AccountInfoUri => new Uri($"{HostUri}/My").AddQuery("expand", "account");
        private async Task<AccountInfoResponse> GetAccountInfo()
        {
            var response = await Client.GetAsync(AccountInfoUri);
            if (!response.IsSuccessStatusCode) throw new AuthenticationException();
            
            var body = await response.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<AccountInfoResponse>(body);

            return parsed;
        }

        
        private Uri DevicesUri => new Uri($"{HostUri}/Accounts/{AccountId}/Devices");
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