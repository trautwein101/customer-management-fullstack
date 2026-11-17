using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {

        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }


        public async ValueTask<bool> TryHandleAsync(
                HttpContext httpContext,
                Exception exception,
                CancellationToken cancellationToken)
        {
            var statusCode = exception switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                //ArgumentException => StatusCodes.Status400BadRequest,
                //UnauthorizedAccessException => StatusCodes.Status403Forbidden,
                //NotImplementedException => StatusCodes.Status501NotImplemented,

                _ => StatusCodes.Status500InternalServerError
            };

            _logger.LogError(
                exception,
                "An error occured while processing the request.");

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = statusCode == 404 ? "Resource not found" : "An unexpected error occured",
                Detail = statusCode == 404 ? exception.Message : "An unexpected server error occured."
            };


            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(
                problem,
                cancellationToken);

            return true;

        }
            






    }
}
