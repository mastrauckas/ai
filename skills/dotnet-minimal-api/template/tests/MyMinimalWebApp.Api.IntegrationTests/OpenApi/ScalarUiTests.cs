namespace MyMinimalWebApp.Api.IntegrationTests.OpenApi;

public sealed class ScalarUiTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task ScalarUi_InDevelopment_ReturnsOk()
    {
        var response = await _client.GetAsync("/scalar/v1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
