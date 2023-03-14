using Application.Dtos;
using Application.Orders.Queries;
using Domain.Repositories;
using Mapster;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Application.Orders.Handlers;

public sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OneOf<OrderDto, NotFound>>
{
    private readonly IOrderRepository orderRepository;

    public GetOrderQueryHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<OneOf<OrderDto, NotFound>> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var result = await orderRepository.GetOrder(query.OrderId);

        return result.Match<OneOf<OrderDto, NotFound>>(
            order => order.Adapt<OrderDto>(),
            notFound => notFound);
    }
}