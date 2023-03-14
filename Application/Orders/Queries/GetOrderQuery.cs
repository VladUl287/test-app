using Application.Dtos;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Application.Orders.Queries;

public sealed class GetOrderQuery : IRequest<OneOf<OrderDto, NotFound>>
{
    public int OrderId { get; init; }
}