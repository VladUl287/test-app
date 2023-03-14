namespace Domain.Entities;

public sealed class OrderItem
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Unit { get; init; } = string.Empty;

    public decimal Quantity { get; init; }

    public int OrderId { get; init; }

    public Order? Order { get; init; }
}