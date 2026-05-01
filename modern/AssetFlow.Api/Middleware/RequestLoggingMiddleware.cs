using System.Diagnostics;

namespace AssetFlow.Api.Middleware;

// THIS IS THE BLOG'S "AFTER" — middleware in Program.cs.
// Compare with legacy Modules/RequestLoggingModule.cs.
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _log;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> log)
    {
        _next = next; _log = log;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        var sw = Stopwatch.StartNew();
        await _next(ctx);
        sw.Stop();
        _log.LogInformation("[modern] {Path} took {Ms}ms",
            ctx.Request.Path, sw.ElapsedMilliseconds);
    }
}