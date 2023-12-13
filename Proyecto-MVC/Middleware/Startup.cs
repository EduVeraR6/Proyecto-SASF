namespace Proyecto_SASF.Middleware
{
    public static class Startup
    {

        public static IApplicationBuilder UseAuthenticationFilter(this IApplicationBuilder app)
            => app.UseMiddleware<JwtAuthorizationFilter>();
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionMiddleware>();
    }
}
