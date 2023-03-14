namespace Application.Dtos;

public sealed class OrderDto
{
    public int Id { get; init; }

    public string Number { get; init; } = string.Empty;

    public DateTime Date { get; init; }

    public ProviderDto? Provider { get; init; }

    public IEnumerable<OrderItemDto> OrderItems { get; init; } = Enumerable.Empty<OrderItemDto>();
}