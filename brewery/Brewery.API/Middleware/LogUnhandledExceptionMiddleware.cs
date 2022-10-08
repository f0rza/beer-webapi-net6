namespace Brewery.API.Middleware
{
    /// <summary>
    /// Logs unhandled exceptions 
    /// </summary>
    public class LogUnhandledExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogUnhandledExceptionMiddleware(ILogger<LogUnhandledExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Request {context.Request?.Method}: {context.Request?.Path.Value} failed");
                throw;
            }
        }
    }
}
