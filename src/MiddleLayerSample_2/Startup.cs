using ExceptionMiddleware_2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionMiddlewareSample_2
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();
      services.AddSwaggerGen();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseCustomExceptionMiddleware((ex,context)=> Process(ex, context));
      }

      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
      }); ;

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    public string Process(Exception ex, HttpContext context)
    {
      string result = "";
      switch (ex.GetType().Name)
      {
        case "ArgumentNullException":
          result = BuildResult(ex.Message);
          context.Response.StatusCode = StatusCodes.Status400BadRequest;
          break;
        case "ValidationException":
          context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
          List<string> errors = ex.Message.Split("\r\n").ToList();
          result = BuildResult(errors);
          break;
        default:
          context.Response.StatusCode = StatusCodes.Status500InternalServerError;
          result = BuildResult(ex.Message);
          break;
      }

      return result;
    }

    private static string BuildResult(dynamic message, string errorCode = "Unhandled Error")
    {
      return JsonConvert.SerializeObject(
          new
          {
            errors = message,//ex.ValidationMessages
            errorCode = errorCode,
            RequestID = "10202"
          });
    }

  }
}
