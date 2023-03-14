using Application.Dtos;
using Application.Orders.Queries;

namespace Presentation.ViewModels;

public sealed class MainOrders
{
    public GetOrdersQuery? Filters { get; init; }

    public IEnumerable<OrdersDto> Orders { get; init; } = Enumerable.Empty<OrdersDto>();

    public IEnumerable<OrdersDto> OrdersNumbers { get; init; } = Enumerable.Empty<OrdersDto>();

    public IEnumerable<ProviderDto> Providers { get; init; } = Enumerable.Empty<ProviderDto>();
}