namespace Log.ExceptionHandling.Globally.IExceptionHandler.WebAPI.Common
{
    public interface IExceptionHandler
    {
        ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken);
    }
}
