using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ExceptionMiddleware_1
{
  public interface IExceptionHandler
  {
    Task<string> Process(Exception ex, HttpContext context);
  }
}
