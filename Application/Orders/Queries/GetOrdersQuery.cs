using Application.Dtos;
using MediatR;

namespace Application.Orders.Queries;

public sealed class GetOrdersQuery : IRequest<IEnumerable<OrdersDto>>
{
    public DateTime? PeriodStart { get; init; }

    public DateTime? PeriodEnd { get; init; }

    public int[]? OrdersIds { get; init; }

    public int[]? ProvidersIds { get; init; }
}