using MyMinimalWebApp.Api.Configuration;

namespace Api.IntegrationTests.Configuration;

public class BuilderConfigurationExtensionsTests
{
    [Fact]
    public void RegisterCors_WithNoAllowedOriginsConfig_UsesEmptyArray()
    {
        // Arrange — create a builder with no configuration sources so
        // GetSection("Cors:AllowedOrigins").Get<string[]>() returns null,
        // exercising the ?? [] fallback branch.
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Configuration.Sources.Clear();

        // Act & Assert — should not throw
        builder.RegisterCors();
        WebApplication app = builder.Build();
        Assert.NotNull(app);
    }
}
