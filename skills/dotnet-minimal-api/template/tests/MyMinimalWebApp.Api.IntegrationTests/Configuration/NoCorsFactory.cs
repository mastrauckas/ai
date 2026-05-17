namespace MyMinimalWebApp.Api.IntegrationTests.Configuration;

public sealed class NoCorsFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }
}
