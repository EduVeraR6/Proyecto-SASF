using System.Text.Json.Serialization;

namespace Proyecto_MVC.Middleware.Models
{
    public class JWTRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
