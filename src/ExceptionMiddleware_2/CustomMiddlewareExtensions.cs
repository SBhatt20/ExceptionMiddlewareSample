using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace ExceptionMiddleware_2
{
  // Extension method used to add the middleware to the HTTP request pipeline.
  public static class CustomMiddlewareExtensions
  {
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder, Func<Exception, HttpContext, string> exceptionHandler)
    {
      return builder.UseMiddleware<ExceptionMiddleware>(exceptionHandler);
    }
  }

}
