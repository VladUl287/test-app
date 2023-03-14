using Application.Orders.Requests;
using Domain.Entities;
using Domain.Repositories;
using MapsterMapper;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Application.Orders.Handlers;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderRequest, OneOf<int, Error<string>>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<OneOf<int, Error<string>>> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = mapper.Map<Order>(request);

        var result = await orderRepository.CreateOrder(order);

        return result.Match<OneOf<int, Error<string>>>(
            order => order.Id,
            error => error);
    }
}