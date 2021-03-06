using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ExceptionMiddleware_1
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IExceptionHandler _exceptionHandler;

    public ExceptionMiddleware(RequestDelegate next, IExceptionHandler exceptionHandler)
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

        result = await _exceptionHandler.Process(ex,context).ConfigureAwait(false);
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(result).ConfigureAwait(false);

        return;
      }
    }

  }

}
