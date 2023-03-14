namespace Domain.Dtos;

public sealed class OrdersFilter
{
    public DateTime? PeriodStart { get; init; }

    public DateTime? PeriodEnd { get; init; }

    public int[]? OrdersIds { get; init; }

    public int[]? ProvidersIds { get; init; }
}