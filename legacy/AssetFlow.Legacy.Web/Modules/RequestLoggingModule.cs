using System;
using System.Diagnostics;
using System.Web;

namespace AssetFlow.Legacy.Web.Modules;

// Compare with modern Middleware/RequestLoggingMiddleware.cs.
public class RequestLoggingModule : IHttpModule
{
    private const string StopwatchKey = "RequestLogging.Stopwatch";

    public void Init(HttpApplication context)
    {
        context.BeginRequest += (s, e) =>
        {
            HttpContext.Current.Items[StopwatchKey] = Stopwatch.StartNew();
        };
        context.EndRequest += (s, e) =>
        {
            var sw = (Stopwatch)HttpContext.Current.Items[StopwatchKey];
            if (sw == null) return;
            sw.Stop();
            var path = HttpContext.Current.Request.Url?.AbsolutePath ?? "?";
            Trace.WriteLine($"[legacy] {path} took {sw.ElapsedMilliseconds}ms");
        };
    }

    public void Dispose() { }
}