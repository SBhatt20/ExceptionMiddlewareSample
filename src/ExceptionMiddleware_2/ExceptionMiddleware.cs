using ExceptionMiddleware_2;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ExceptionMiddleware_2
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly Func<Exception, HttpContext, string> _exceptionHandler;

    public ExceptionMiddleware(RequestDelegate next, Func<Exception, HttpContext, string> exceptionHandler)
    {
      _next = next ?? throw new ArgumentNullException(nameof(next));
      _exceptionHandler = exceptionHandler;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        if (context.Response.HasStarted)
        {
          throw;
        }

        string result;
        context.Response.Clear();

        result = _exceptionHandler(ex,context);
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(result).ConfigureAwait(false);

        return;
      }
    }

  }

}
