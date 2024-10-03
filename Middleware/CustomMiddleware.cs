using Microsoft.AspNetCore.Http;

namespace PracticeProject.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Request Path: " + context.Request.Path);

            await _next(context);

            Console.WriteLine("Response Status Code: " + context.Response.StatusCode);
        }
        }
    }
