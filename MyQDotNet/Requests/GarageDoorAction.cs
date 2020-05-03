using Newtonsoft.Json;

namespace MyQDotNet.Requests
{
    public class GarageDoorAction
    {
        [JsonProperty("action_type")]
        public string ActionType { get; set; }
        
        public GarageDoorAction() {}

        public GarageDoorAction(string actionType)
        {
            ActionType = actionType;
        }
    }
}