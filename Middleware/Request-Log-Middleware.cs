using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace _23WebC_Nhom10.Middleware
{
    public class Request_Log_Middleware
    {
        private readonly RequestDelegate _next;
        public Request_Log_Middleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            var request = context.Request.Path;
            var datetime = DateTime.Now;
            var IP = context.Connection.RemoteIpAddress;
            Console.WriteLine($"Request Information:{context.Request.Path}");
            Console.WriteLine($"Request Time:{DateTime.Now}");
            Console.WriteLine($"Request IP:{context.Connection.RemoteIpAddress}");
            Log.Information("Request:{request} - Datetime:{datetime} - IP:{IP}", request, datetime, IP);
            await _next(context);
        }

    }
}

