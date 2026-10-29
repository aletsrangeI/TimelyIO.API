using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WatchDog;

namespace Logging;

public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;
    private readonly bool _isWatchDogEnabled;

    public LoggerAdapter(ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        _logger = loggerFactory.CreateLogger<T>();
        _isWatchDogEnabled = configuration.GetValue<bool>("WatchDog:Enabled", false);
    }

    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
        if (_isWatchDogEnabled)
        {
            try { WatchLogger.Log(message); } catch {}
        }
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
        if (_isWatchDogEnabled)
        {
            try { WatchLogger.Log(message); } catch {}
        }
    }

    public void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);
        if (_isWatchDogEnabled)
        {
            try { WatchLogger.Log(message); } catch {}
        }
    }
}