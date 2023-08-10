using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomAutomaticStatusCodesMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAutomaticStatusCodesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            ErrorWrapper Wrapper = new ErrorWrapper();
            ErrorResponse errorResponse = new ErrorResponse();
            Error error = new Error();

            httpContext.Response.ContentType = MediaTypeNames.Application.Json;            
            await _next(httpContext);
            switch (httpContext.Response.StatusCode)
            {
                case StatusCodes.Status415UnsupportedMediaType:
                    error = new Error("415", "Hi I am a 415 HTTP Error :)");
                    errorResponse.errors.Add(error);
                    Wrapper.response = errorResponse;
                    await httpContext.Response.WriteAsync(Wrapper.ToString());
                    Wrapper.response.errors.Clear();
                    break;
                case StatusCodes.Status401Unauthorized:
                    error = new Error("401", "Hi I am a 401 HTTP Error :)");
                    errorResponse.errors.Add(error);
                    Wrapper.response = errorResponse;
                    await httpContext.Response.WriteAsync(Wrapper.ToString());
                    Wrapper.response.errors.Clear();
                    break;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomAutomaticStatusCodesMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAutomaticStatusCodesMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAutomaticStatusCodesMiddleware>();
        }
    }
}
