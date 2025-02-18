﻿namespace eCommerce.API.MiddleWares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Handling request:" + context.Request.Path);
            await _next(context);
            Console.WriteLine("Finished handling request.");
        }
    }
}
