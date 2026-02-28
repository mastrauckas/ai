namespace Api.Endpoints;

public static class ItemEndpointExtensions
{
    extension(WebApplication app)
    {
        public void MapItemEndpoints(RouteGroupBuilder root)
        {
            var group = root.MapGroup("/items").WithTags("Items");

            group.MapGet("/", GetAllItems)
                .WithName("GetAllItems")
                .WithSummary("Get all items")
                .Produces<IEnumerable<ItemDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/{id:int}", GetItemById)
                .WithName("GetItemById")
                .WithSummary("Get an item by ID")
                .Produces<ItemDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", CreateItem)
                .WithName("CreateItem")
                .WithSummary("Create a new item")
                .Produces<ItemDto>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id:int}", UpdateItem)
                .WithName("UpdateItem")
                .WithSummary("Update an item")
                .Produces<ItemDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id:int}", DeleteItem)
                .WithName("DeleteItem")
                .WithSummary("Delete an item")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);
        }
    }

    private static async Task<Ok<IEnumerable<ItemDto>>> GetAllItems(IItemService service)
    {
        var items = await service.GetAllAsync();
        return TypedResults.Ok(items);
    }

    private static async Task<Results<Ok<ItemDto>, NotFound>> GetItemById(
        int id,
        IItemService service)
    {
        var item = await service.GetByIdAsync(id);
        return item is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(item);
    }

    private static async Task<Created<ItemDto>> CreateItem(
        CreateItemRequest request,
        IItemService service)
    {
        var item = new ItemDto(0, request.Name, request.Description);
        var created = await service.CreateAsync(item);
        return TypedResults.Created($"/items/{created.Id}", created);
    }

    private static async Task<Results<Ok<ItemDto>, NotFound>> UpdateItem(
        int id,
        UpdateItemRequest request,
        IItemService service)
    {
        var item = new ItemDto(0, request.Name, request.Description);
        var updated = await service.UpdateAsync(id, item);
        return updated is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(updated);
    }

    private static async Task<Results<NoContent, NotFound>> DeleteItem(
        int id,
        IItemService service)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}

