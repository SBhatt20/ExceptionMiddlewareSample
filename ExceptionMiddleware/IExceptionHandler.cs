using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionMiddleware
{
  public interface IExceptionHandler
  {
    Task<string> Process(Exception ex, HttpContext context);
  }
}
