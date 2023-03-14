using Application.Dtos;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Application.Orders.Requests;

public sealed class CreateOrderRequest : IRequest<OneOf<int, Error<string>>>
{
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int ProviderId { get; set; }
    public OrderItemDto[] OrderItems { get; set; } = Array.Empty<OrderItemDto>();
}