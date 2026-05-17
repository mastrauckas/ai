namespace MyMinimalWebApp.Api.IntegrationTests.OpenApi;

public sealed class ProductionFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Environments.Production);
    }
}
