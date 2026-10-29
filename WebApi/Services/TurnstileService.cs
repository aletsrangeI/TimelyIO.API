using System.Text.Json;
using System.Text.Json.Serialization;
using Interface.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApi.Services;

public class TurnstileService : ITurnstileService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<TurnstileService> _logger;

    public TurnstileService(HttpClient httpClient, IConfiguration configuration, ILogger<TurnstileService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> VerifyTokenAsync(string? token, string? remoteIp = null, string? expectedAction = "reserva", CancellationToken cancellationToken = default)
    {
        var isEnabled = _configuration.GetValue<bool>("Turnstile:Enabled", true);
        var secretKey = _configuration["Turnstile:SecretKey"] ?? Environment.GetEnvironmentVariable("TURNSTILE_SECRET");

        // En desarrollo local o si Turnstile esta deshabilitado o sin llave, permitir bypass controlado
        if (!isEnabled || string.IsNullOrWhiteSpace(secretKey))
        {
            _logger.LogInformation("[Turnstile] Bypass activo (deshabilitado o sin SecretKey configurada).");
            return true;
        }

        if (string.IsNullOrWhiteSpace(token) || token.Length > 2048)
        {
            _logger.LogWarning("[Turnstile] Token vacio o excede 2048 caracteres.");
            return false;
        }

        // Tokens de prueba oficiales de Cloudflare o dummy local
        if (token == "1x0000000000000000000000000000000AA" || token.StartsWith("test-token-") || token == "cf-turnstile-dummy-pass")
        {
            return true;
        }

        try
        {
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(TimeSpan.FromSeconds(10));

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "secret", secretKey },
                { "response", token },
                { "remoteip", remoteIp ?? string.Empty }
            });

            var verifyUrl = _configuration["Turnstile:VerifyUrl"] ?? "https://challenges.cloudflare.com/turnstile/v0/siteverify";
            var response = await _httpClient.PostAsync(verifyUrl, content, timeoutCts.Token);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("[Turnstile] Error HTTP de Cloudflare: {StatusCode}", response.StatusCode);
                return false;
            }

            var json = await response.Content.ReadAsStringAsync(timeoutCts.Token);
            var result = JsonSerializer.Deserialize<TurnstileVerifyResponse>(json);

            if (result == null || !result.Success)
            {
                _logger.LogWarning("[Turnstile] Verificacion rechazada por Cloudflare: {Errors}", string.Join(", ", result?.ErrorCodes ?? Array.Empty<string>()));
                return false;
            }

            // Validar action
            if (!string.IsNullOrEmpty(expectedAction) && !string.IsNullOrEmpty(result.Action) &&
                !string.Equals(result.Action, expectedAction, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning("[Turnstile] Action mismatch. Esperado: {Expected}, Recibido: {Actual}", expectedAction, result.Action);
                return false;
            }

            // Validar hostnames permitidos si estan configurados
            var allowedHostnames = _configuration.GetSection("Turnstile:AllowedHostnames").Get<string[]>();
            if (allowedHostnames != null && allowedHostnames.Length > 0 && !string.IsNullOrEmpty(result.Hostname))
            {
                if (!allowedHostnames.Contains(result.Hostname, StringComparer.OrdinalIgnoreCase))
                {
                    _logger.LogWarning("[Turnstile] Hostname no autorizado: {Hostname}. Permitidos: {Allowed}", result.Hostname, string.Join(",", allowedHostnames));
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[Turnstile] Excepcion al verificar token con Cloudflare.");
            return false;
        }
    }

    private class TurnstileVerifyResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error-codes")]
        public string[]? ErrorCodes { get; set; }

        [JsonPropertyName("action")]
        public string? Action { get; set; }

        [JsonPropertyName("hostname")]
        public string? Hostname { get; set; }

        [JsonPropertyName("challenge_ts")]
        public string? ChallengeTs { get; set; }
    }
}
