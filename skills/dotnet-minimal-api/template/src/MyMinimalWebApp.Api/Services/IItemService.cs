namespace MyMinimalWebApp.Api.Services;

public interface IItemService
{
    public Task<IEnumerable<ItemDto>> GetAllAsync(
        CancellationToken cancellationToken = default);
    public Task<ItemDto?> GetByIdAsync(int id,
        CancellationToken cancellationToken = default);
    public Task<ItemDto> CreateAsync(ItemDto item,
        CancellationToken cancellationToken = default);
    public Task<ItemDto?> UpdateAsync(int id,
        ItemDto item,
        CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(int id,
        CancellationToken cancellationToken = default);
}
