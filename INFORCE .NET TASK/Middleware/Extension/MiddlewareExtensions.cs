using INFORCE_.NET_TASK.Middleware.MiddlewareClasses;

namespace INFORCE_.NET_TASK.Middleware.Extension
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionsHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HandlingExceptionsMiddleware>();
        }
    }
}
