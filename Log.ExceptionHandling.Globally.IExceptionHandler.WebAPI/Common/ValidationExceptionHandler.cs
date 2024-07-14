using System.ComponentModel.DataAnnotations;

namespace Log.ExceptionHandling.Globally.IExceptionHandler.WebAPI.Common
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is ValidationException validationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(validationException.ValidationResult, cancellationToken);

                return true;
            }

            return false;
        }
    }
}
