using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Primitives;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.


//***********************************************************************************
app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        //if (context.Request.Query.TryGetValue("debug", out StringValues queryStrs)
        //        && queryStrs.FirstOrDefault() == "true") //Microsoft.Extensions.Primitives.StringValues
        //if (context.Request.Query["debug"] != "")
        //    return;

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var ex = context.Features.Get<IExceptionHandlerFeature>();
        if (ex is not null)
            await context.Response.WriteAsync(ex.Error.Message);
    });
});
//***********************************************************************************

/*//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0#exception-handler-page
 app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // using static System.Net.Mime.MediaTypeNames;
            context.Response.ContentType = Text.Plain;

            await context.Response.WriteAsync("An exception was thrown.");

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                await context.Response.WriteAsync(" The file was not found.");
            }

            if (exceptionHandlerPathFeature?.Path == "/")
            {
                await context.Response.WriteAsync(" Page: Home.");
            }
        });
    });
 */





app.UseAuthorization();

app.MapControllers();

app.Run();
