using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.HasOne(x => x.Provider)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.Date });

        builder.HasIndex(x => new { x.Number, x.ProviderId })
            .IsUnique();
    }
}