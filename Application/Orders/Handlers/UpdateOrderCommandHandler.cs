using Application.Orders.Requests;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Application.Orders.Handlers;

public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderRequest, OneOf<Success, NotFound>>
{
    private readonly IOrderRepository orderRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<OneOf<Success, NotFound>> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = request.Adapt<Order>();

        return await orderRepository.UpdateOrder(order);
    }
}