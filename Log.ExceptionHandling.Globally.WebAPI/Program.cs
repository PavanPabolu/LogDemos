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






app.UseAuthorization();

app.MapControllers();

app.Run();
