using AGE.Middleware.Exceptions.Unauthorized;
using AGE.Middleware.Models;
using AGE.Utils;
using System.Text;
using System.Text.Json;

public class JwtAuthorizationFilter
{
    private readonly RequestDelegate _next;
    private readonly HttpClient _httpClient;
    private string mensajeError = "Token de autorización inválido o faltante. Por favor, proporcione un token válido.";

    public JwtAuthorizationFilter(RequestDelegate next, IHttpClientFactory httpClientFactory)
    {
        _next = next;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var token = ExtractTokenFromHeader(context);
            await ValidateTokenAsync(token);
            await _next(context);
        }
        catch (UnauthorizedException)
        {
            throw new UnauthorizedException(mensajeError);
        }
    }

    private string ExtractTokenFromHeader(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            throw new UnauthorizedException(mensajeError);

        return authorizationHeader.Substring("Bearer ".Length).Trim();
    }

    private async Task ValidateTokenAsync(string token)
    {
        var jwtRequest = new JWTRequest { Token = token };
        var jsonData = JsonSerializer.Serialize(jwtRequest);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(Environment.GetEnvironmentVariable("VALIDATION_API_URL"), content);

        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedException(mensajeError);
    }
}
