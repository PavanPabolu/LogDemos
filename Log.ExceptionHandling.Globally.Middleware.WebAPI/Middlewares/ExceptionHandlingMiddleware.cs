﻿using Log.ExceptionHandling.Globally.Middleware.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Log.ExceptionHandling.Globally.Middleware.WebAPI.Middlewares
{
    //public class ExceptionHandlingMiddleware : IMiddleware
    //{
    //}

    public class ExceptionHandlingMiddleware //: IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;

            var errorResponse = new ErrorResponse { Success = false }

            switch (ex)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid Token"))
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = ex.Message;
                    break;
            }

            _logger.LogError(ex.Message);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);

        }
    }
}
