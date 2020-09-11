using Microsoft.AspNetCore.Builder;

namespace ExceptionMiddleware_1
{
  // Extension method used to add the middleware to the HTTP request pipeline.
  public static class CustomMiddlewareExtensions
  {
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<ExceptionMiddleware>();
    }
  }

}
