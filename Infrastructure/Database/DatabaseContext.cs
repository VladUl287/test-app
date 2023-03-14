using Domain.Entities;
using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class DatabaseContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Provider> Providers => Set<Provider>();

    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new ProviderConfiguration());

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasData(new Provider[]
            {
                new Provider
                {
                    Id = 1,
                    Name = "Provider Test First One"
                },
                new Provider
                {
                    Id = 2,
                    Name = "Provider Test Second One"
                }
            });
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasData(new Order[]
            {
                new Order
                {
                    Id = 1,
                    Number = "123456789",
                    Date = DateTime.Now.AddDays(-1),
                    ProviderId = 1
                },
                new Order
                {
                    Id = 2,
                    Number = "987654321",
                    Date = DateTime.Now.AddDays(-2),
                    ProviderId = 1
                },
                new Order
                {
                    Id = 3,
                    Number = "abcdefgxyz",
                    Date = DateTime.Now.AddDays(-3),
                    ProviderId = 2
                }
            });
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasData(new OrderItem[]
            {
                new OrderItem
                {
                    Id = 1,
                    Name = "Item 1",
                    OrderId = 1,
                    Unit = "unit 657453",
                    Quantity = 1.3M
                },
                new OrderItem
                {
                    Id = 2,
                    Name = "Item 2",
                    OrderId = 1,
                    Unit = "unit 3453434",
                    Quantity = 123
                },
                new OrderItem
                {
                    Id = 3,
                    Name = "Item 3",
                    OrderId = 1,
                    Unit = "unit 546234",
                    Quantity = 1
                },
                new OrderItem
                {
                    Id = 4,
                    Name = "Item 4",
                    OrderId = 2,
                    Unit = "unit 12312",
                    Quantity = 5.6M
                },
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}