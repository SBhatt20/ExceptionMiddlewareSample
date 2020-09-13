# ExceptionMiddlewareSample
How to apply custom Exception Middleware (.net core) 
How to write test cases of Middleware

# There are 2 samples to show that how we can create / use custom Exception Middleware
-ExceptionMiddleware_1
In the solution  you need to implement IExceptionHandler as a custom Exception Handler and Exception Handler will call the Process Method of Object (which implements IExceptionHandler).
You can refer ExceptionMiddlewareSample_1.Api Project to see the Implementation of IExceptionHandler

IExceptionHandler Implementation

 public class MiddlewareExceptionHandler : IExceptionHandler
  {
    public async Task<string> Process(Exception ex, HttpContext context)
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

StartUp.cs
 app.UseCustomExceptionMiddleware();

#ExceptionMiddleware_2
In the solution you need to provide Func<> (delegate) to process the Exceptions.
You can refer ExceptionMiddlewareSample_2.Api Project to see the implementation.

StartUp.cs
 app.UseCustomExceptionMiddleware((ex,context)=> Process(ex, context));
 
 Create a new Method  

# NuGet
https://www.nuget.org/packages/ExceptionMiddleware_1/1.0.0

Package Manager
Install-Package ExceptionMiddleware_1 -Version 1.0.0
