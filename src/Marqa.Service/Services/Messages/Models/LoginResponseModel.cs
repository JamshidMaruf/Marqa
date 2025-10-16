using Newtonsoft.Json;

namespace Marqa.Service.Services.Messages.Models;

public class LoginResponseModel
{
    [JsonProperty("message")]
    public string Message { get; set; }
    
    [JsonProperty("data")]
    public DataInfo Data { get; set; }
    
    [JsonProperty("token_type")]
    public string TokenType { get; set; }
    
    public class DataInfo
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}