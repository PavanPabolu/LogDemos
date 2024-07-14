using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace Log.ExceptionHandling.Globally.IExceptionHandler.WebAPI.Common
{
    public class BadExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BadExceptionHandler> logger;

        public BadExceptionHandler(ILogger<BadExceptionHandler> logger)
        {
            this.logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            this.logger.LogError(exception, "Bad Exception");

            if (exception is not BadHttpRequestException badRequestException)
            {
                return false;
            }

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "An unexpected error occurred",
                Detail = exception.Message,
            }, cancellationToken: cancellationToken);
            return true;
        }
    }
}
