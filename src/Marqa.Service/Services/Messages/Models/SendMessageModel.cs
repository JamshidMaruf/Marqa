using Newtonsoft.Json;

namespace Marqa.Service.Services.Messages.Models;

public class SendMessageModel
{
    [JsonProperty("mobile_phone")]
    public string Phone { get; set; }
    
    [JsonProperty("message")]
    public string Message { get; set; }
    
    [JsonProperty("from")]
    public string From { get; set; }
}