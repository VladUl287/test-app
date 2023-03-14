using Application.Orders.Requests;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Handlers;

public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderRequest>
{
    private readonly IOrderRepository orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        await orderRepository.DeleteOrder(request.OrderId);
    }
}