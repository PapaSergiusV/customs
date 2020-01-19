using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace customs
{
    public class PHPFilterMiddleware
    {
        private readonly RequestDelegate next;

        public PHPFilterMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("php"))
            {
                context.Response.StatusCode = 404;
                await context.Response.StartAsync();
            }
            else
                await next.Invoke(context);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UsePHPFilter(this IApplicationBuilder app) =>
            app.UseMiddleware<PHPFilterMiddleware>();
    }
}
