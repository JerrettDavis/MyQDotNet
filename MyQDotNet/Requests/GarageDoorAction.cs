using Newtonsoft.Json;

namespace MyQDotNet.Requests
{
    public class GarageDoorAction
    {
        [JsonProperty("action_type")]
        public string ActionType { get; set; } = null!;
        
        public GarageDoorAction() {}

        public GarageDoorAction(string actionType)
        {
            ActionType = actionType;
        }
    }
}