using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ExceptionMiddlewareSample_1.Api.Controllers
{
  [Produces("application/vnd.hal+json")]
  [Route("[controller]")]
  public class ExceptionMiddlewareController : ControllerBase
  {
    private readonly ILogger<ExceptionMiddlewareController> _logger;

    public ExceptionMiddlewareController(ILogger<ExceptionMiddlewareController> logger)
    {
      _logger = logger;
    }

    [Produces("application/vnd.hal+json", "application / json")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      throw new Exception("This is a new test exception");
    }

    [Produces("application/vnd.hal+json", "application / json")]
    [Route("GetNew/{id}"), HttpGet]
    public async Task<IActionResult> GetNew(string id="1")
    {
      throw new Exception("This is a another test exception");
    }
  }
}
