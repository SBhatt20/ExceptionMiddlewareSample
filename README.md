# ExceptionMiddlewareSample
How to apply custom Exception Middleware (.net core) 
How to write test cases of Middleware

# There are 2 samples to show that how we can create / use custom Exception Middleware
#ExceptionMiddleware_1
In the solution  you need to implement IExceptionHandler as a custom Exception Handler and Exception Handler will call the Process Method of Object (which implements IExceptionHandler).
You can refer ExceptionMiddlewareSample_1.Api Project to see the Implementation of IExceptionHandler


#ExceptionMiddleware_2
In the solution you need to provide Func<> (delegate) to process the Exceptions.
You can refer ExceptionMiddlewareSample_2.Api Project to see the implementation.
