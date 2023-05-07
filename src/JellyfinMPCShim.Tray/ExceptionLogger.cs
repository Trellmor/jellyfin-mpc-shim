using Microsoft.Extensions.Logging;

namespace JellyfinMPCShim.Tray;
internal class ExceptionLogger
{
    private readonly ILogger<ExceptionLogger> _logger;

    public ExceptionLogger(ILogger<ExceptionLogger> logger)
    {
        _logger = logger;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
    }

    private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var ex = e.ExceptionObject as Exception;
        if (ex != null)
        {
            _logger.LogCritical(ex, "Unhandled exception {message}", ex.Message);
        }
    }
}
