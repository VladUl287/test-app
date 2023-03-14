using Application.Dtos;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Application.Orders.Requests;

public sealed class UpdateOrderRequest : IRequest<OneOf<Success, NotFound>>
{
    public int Id { get; init; }
    public string Number { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public int ProviderId { get; init; }
    public OrderItemDto[] OrderItems { get; init; } = Array.Empty<OrderItemDto>();
}