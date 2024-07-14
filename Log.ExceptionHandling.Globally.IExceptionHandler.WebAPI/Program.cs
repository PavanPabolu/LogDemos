using Microsoft.AspNetCore.Diagnostics;
using Log.ExceptionHandling.Globally.IExceptionHandler.WebAPI.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<BadExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.UseExceptionHandler((_ => { }));

app.MapGet("api/exception", () =>
{
    throw new Exception("Error Occured");
});

app.MapGet("api/badexception", () =>
{
    throw new BadHttpRequestException("Bad Request");
});



app.Run();

