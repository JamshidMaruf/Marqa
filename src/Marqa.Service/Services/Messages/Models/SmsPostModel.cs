using Newtonsoft.Json;

namespace Marqa.Service.Services.Messages.Models;

public class SmsPostModel
{
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("password")]
    public string SecretKey { get; set; }
}