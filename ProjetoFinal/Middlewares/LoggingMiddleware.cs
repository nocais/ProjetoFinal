// Log das requisições

using System.Diagnostics;

namespace ProjetoFinal.Middlewares;

public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        context.Items["StartTime"] = DateTime.Now;
        await next(context);
        sw.Stop();
        logger.LogInformation(context.Request.Method, context.Request.Path, context.Response.StatusCode, sw.ElapsedMilliseconds);
    }
}