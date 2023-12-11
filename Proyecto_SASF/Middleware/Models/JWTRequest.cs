using System.Text.Json.Serialization;

namespace AGE.Middleware.Models
{
    public class JWTRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
