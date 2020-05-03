using System.Text.Json.Serialization;

namespace MyQDotNet.Requests
{
    public class GarageDoorAction
    {
        [JsonPropertyName("action_type")]
        public string ActionType { get; set; }
        
        public GarageDoorAction() {}

        public GarageDoorAction(string actionType)
        {
            ActionType = actionType;
        }
    }
}