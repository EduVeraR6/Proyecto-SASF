using System.Text.Json.Serialization;

namespace Proyecto_SASF.Middleware.Models
{
    public class JWTRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
