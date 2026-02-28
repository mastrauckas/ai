using System.Net;
using System.Net.Http.Json;
using Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.Tests.Endpoints;

public class ItemEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ItemEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllItems_ReturnsOkWithItems()
    {
        // Act
        var response = await _client.GetAsync("/items");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var items = await response.Content.ReadFromJsonAsync<IEnumerable<Item>>();
        Assert.NotNull(items);
        Assert.NotEmpty(items);
    }

    [Fact]
    public async Task GetItemById_WithValidId_ReturnsOkWithItem()
    {
        // First get all items to find a valid ID
        var getAllResponse = await _client.GetAsync("/items");
        var items = await getAllResponse.Content.ReadFromJsonAsync<IEnumerable<Item>>();
        var itemId = items?.First().Id;
        
        if (itemId == null)
            return;

        // Act
        var response = await _client.GetAsync($"/items/{itemId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var item = await response.Content.ReadFromJsonAsync<Item>();
        Assert.NotNull(item);
        Assert.Equal(itemId, item.Id);
    }

    [Fact]
    public async Task GetItemById_WithInvalidId_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/items/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateItem_WithValidRequest_ReturnsCreatedWithItem()
    {
        // Arrange
        var request = new { name = "New Item", description = "A new item" };

        // Act
        var response = await _client.PostAsJsonAsync("/items", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var item = await response.Content.ReadFromJsonAsync<Item>();
        Assert.NotNull(item);
        Assert.Equal("New Item", item.Name);
        Assert.Equal("A new item", item.Description);
    }

    [Fact]
    public async Task CreateItem_WithoutName_ReturnsBadRequest()
    {
        // Arrange
        var request = new { name = "", description = "A new item" };

        // Act
        var response = await _client.PostAsJsonAsync("/items", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateItem_WithValidRequest_ReturnsOkWithUpdatedItem()
    {
        // First get all items to find a valid ID
        var getAllResponse = await _client.GetAsync("/items");
        var items = await getAllResponse.Content.ReadFromJsonAsync<IEnumerable<Item>>();
        var itemId = items?.First().Id;
        
        if (itemId == null)
            return;

        // Arrange
        var request = new { name = "Updated Item", description = "Updated description" };

        // Act
        var response = await _client.PutAsJsonAsync($"/items/{itemId}", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var item = await response.Content.ReadFromJsonAsync<Item>();
        Assert.NotNull(item);
        Assert.Equal("Updated Item", item.Name);
        Assert.Equal("Updated description", item.Description);
    }

    [Fact]
    public async Task UpdateItem_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var request = new { name = "Updated Item", description = "Updated description" };

        // Act
        var response = await _client.PutAsJsonAsync("/items/999", request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateItem_WithoutName_ReturnsBadRequest()
    {
        // First get all items to find a valid ID
        var getAllResponse = await _client.GetAsync("/items");
        var items = await getAllResponse.Content.ReadFromJsonAsync<IEnumerable<Item>>();
        var itemId = items?.First().Id;
        
        if (itemId == null)
            return;

        // Arrange
        var request = new { name = "", description = "Updated description" };

        // Act
        var response = await _client.PutAsJsonAsync($"/items/{itemId}", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteItem_WithValidId_ReturnsNoContent()
    {
        // First, get all items to know what ID to delete
        var getResponse = await _client.GetAsync("/items");
        var items = await getResponse.Content.ReadFromJsonAsync<IEnumerable<Item>>();
        var itemToDelete = items?.First();
        
        if (itemToDelete == null)
            return;

        // Act
        var response = await _client.DeleteAsync($"/items/{itemToDelete.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify it's deleted
        var getAfterDelete = await _client.GetAsync($"/items/{itemToDelete.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getAfterDelete.StatusCode);
    }

    [Fact]
    public async Task DeleteItem_WithInvalidId_ReturnsNotFound()
    {
        // Act
        var response = await _client.DeleteAsync("/items/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
