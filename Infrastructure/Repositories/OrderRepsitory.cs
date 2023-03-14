using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace Infrastructure.Repositories;

public sealed class OrderRepsitory : IOrderRepository
{
    private readonly DatabaseContext databaseContext;

    public OrderRepsitory(DatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public async Task<IEnumerable<Order>> GetOrders(OrdersFilter filter)
    {
        var query = databaseContext.Orders.AsNoTracking();

        query = SetFilters(filter, query);

        return await query.ToListAsync();
    }

    private static IQueryable<Order> SetFilters(OrdersFilter filter, IQueryable<Order> query)
    {
        if (filter.PeriodStart.HasValue)
        {
            query = query.Where(x => x.Date >= filter.PeriodStart);
        }

        if (filter.PeriodEnd.HasValue)
        {
            query = query.Where(x => x.Date <= filter.PeriodEnd);
        }

        if (filter.OrdersIds?.Length > 0)
        {
            query = query.Where(x => filter.OrdersIds.Contains(x.Id));
        }

        if (filter.ProvidersIds?.Length > 0)
        {
            query = query.Where(x => filter.ProvidersIds.Contains(x.ProviderId));
        }

        return query;
    }

    public async Task<OneOf<Order, NotFound>> GetOrder(int id)
    {
        var order = await databaseContext.Orders
            .Include(x => x.OrderItems)
            .Include(x => x.Provider)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order is null)
        {
            return new NotFound();
        }

        return order;
    }

    public async Task<OneOf<Order, Error<string>>> CreateOrder(Order order)
    {
        var exists = await databaseContext.Orders
            .AnyAsync(x => x.Number == order.Number && x.ProviderId == order.ProviderId);

        if (exists)
        {
            return new Error<string>("Заказ с таким номером и поставщиком уже существует.");
        }

        await databaseContext.Orders.AddAsync(order);
        await databaseContext.SaveChangesAsync();

        return order;
    }

    public async Task<OneOf<Success, NotFound>> UpdateOrder(Order order)
    {
        var dbOrder = await databaseContext.Orders
            .Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == order.Id);

        if (dbOrder is null)
        {
            return new NotFound();
        }

        dbOrder.Date = order.Date;
        dbOrder.Number = order.Number;
        dbOrder.ProviderId = order.ProviderId;
        dbOrder.OrderItems = order.OrderItems;

        await databaseContext.SaveChangesAsync();

        return new Success();
    }

    public async Task DeleteOrder(int id)
    {
        var order = await databaseContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order is not null)
        {
            databaseContext.Orders.Remove(order);
            await databaseContext.SaveChangesAsync();
        }
    }
}