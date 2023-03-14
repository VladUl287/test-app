using Domain.Dtos;
using Domain.Entities;
using OneOf;
using OneOf.Types;

namespace Domain.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrders(OrdersFilter filter);

    Task<OneOf<Order, NotFound>> GetOrder(int id);

    Task<OneOf<Order, Error<string>>> CreateOrder(Order order);

    Task<OneOf<Success, NotFound>> UpdateOrder(Order order);

    Task DeleteOrder(int id);
}