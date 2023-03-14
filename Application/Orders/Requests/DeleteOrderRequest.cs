using MediatR;

namespace Application.Orders.Requests;

public sealed class DeleteOrderRequest : IRequest
{
    public int OrderId { get; init; }
}