using Application.Dtos;
using Application.Orders.Queries;
using Domain.Dtos;
using Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace Application.Orders.Handlers;

public sealed class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrdersDto>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<OrdersDto>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var ordersFilter = mapper.Map<OrdersFilter>(query);

        var orders = await orderRepository.GetOrders(ordersFilter);

        return mapper.Map<IEnumerable<OrdersDto>>(orders);
    }
}