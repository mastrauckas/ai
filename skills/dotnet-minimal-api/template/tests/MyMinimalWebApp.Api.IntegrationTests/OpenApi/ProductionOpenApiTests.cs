namespace MyMinimalWebApp.Api.IntegrationTests.OpenApi;

public sealed class ProductionOpenApiTests(ProductionFactory factory)
    : IClassFixture<ProductionFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Theory]
    [InlineData("/openapi/v1.json")]
    [InlineData("/scalar/v1")]
    public async Task OpenApiAndScalar_InProduction_ReturnNotFound(
        string path)
    {
        var response = await _client.GetAsync(path);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
