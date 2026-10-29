namespace Interface.Services;

public interface ITurnstileService
{
    Task<bool> VerifyTokenAsync(string? token, string? remoteIp = null, string? expectedAction = "reserva", CancellationToken cancellationToken = default);
}
