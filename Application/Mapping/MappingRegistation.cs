using Application.Dtos;
using Application.Orders.Requests;
using Domain.Dtos;
using Domain.Entities;
using Mapster;

namespace Application.Mapping;

public sealed class MappingRegistation : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>();
        config.NewConfig<Order, OrdersDto>();
        config.NewConfig<OrderItem, OrderItemDto>();
        config.NewConfig<Provider, ProviderDto>();

        config.NewConfig<OrdersFilter, CreateOrderRequest>();

        config.NewConfig<Order, CreateOrderRequest>();
    }
}