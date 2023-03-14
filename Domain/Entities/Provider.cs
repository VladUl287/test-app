namespace Domain.Entities;

public sealed class Provider
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public ICollection<Order>? Orders { get; init; }
}