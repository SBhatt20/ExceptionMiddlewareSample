using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ExceptionMiddleware_2;

namespace MIddlewareTest
{
  public class UnitTest1
  {

    [Fact]
    public async Task Test_CustomExceptionMiddleWareResponse_Success()
    {
      //Arrange
      var context = new DefaultHttpContext();
      context.Response.Body = new MemoryStream();

      var middleware = new ExceptionMiddleware(next: (a) =>
       {
         throw new Exception("This is a custom message");
       },
      (ex, context) => Process(ex, context));


      //Act
      await middleware.Invoke(context);

      context.Response.Body.Seek(0, SeekOrigin.Begin);
      var reader = new StreamReader(context.Response.Body);
      var streamText = await reader.ReadToEndAsync().ConfigureAwait(false);
      var objResponse = JsonConvert.DeserializeObject<CustomErrorResponse>(streamText);

      //Validate
      Assert.True(context.Response.StatusCode == StatusCodes.Status500InternalServerError);
      Assert.True(objResponse.CustomStatusCode == 999);
      Assert.True(objResponse.CustomMessage == "This is a custom message.");
      Assert.True(objResponse.CustomRequestID == "CustomRequestIs-999");
    }

    public string Process(Exception ex, HttpContext context)
    {
      context.Response.StatusCode = StatusCodes.Status500InternalServerError;

      return JsonConvert.SerializeObject(
          new CustomErrorResponse
          {
            CustomMessage = ex.Message,
            CustomStatusCode = 999,
            CustomRequestID = "CustomRequestIs-999"
          });
    }
  }

  public class CustomErrorResponse
  {
    public int CustomStatusCode { get; set; }
    public string CustomMessage { get; set; }
    public string CustomRequestID { get; set; }
  }

}
