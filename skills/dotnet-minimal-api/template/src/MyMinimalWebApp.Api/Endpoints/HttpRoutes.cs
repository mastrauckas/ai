namespace MyMinimalWebApp.Api.Endpoints;

internal static class HttpRoutesExtensions
{
    extension(WebApplication app)
    {
        public void ConfigureHttpRoutes()
        {
            var root = app
                .MapGroup("api")
                .RequireRateLimiting("fixed");

            app.MapItemEndpoints(root);
        }
    }
}
