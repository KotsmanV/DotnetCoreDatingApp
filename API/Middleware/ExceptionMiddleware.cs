using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        /// <summary>
        /// Used to handle any exception, either in production or development mode
        /// </summary>
        /// <param name="next">Handles what's next in the Http request</param>
        /// <param name="logger">The logger instance which will record any exceptions</param>
        /// <param name="env">Specifies which is the production environment</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }


        /// <summary>
        /// If the Http context throws any exceptions, they will be handled by this method.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //Waits for all the middleware to execute.
                //They will in turn throw the exception to upper levels,
                //until it reaches somewhere it can be handled.
                await next(context);
            }
            //This middleware will be on top of everything,
            //so it will catch any exception from anywhere.
            catch (Exception ex)
            {
                //Log the exception in the terminal
                logger.LogError(ex, ex.Message);
                //specify the response content type
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //Create a response which checks if the app is in development or not.
                var response = env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                //Set the Json options and create it
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
