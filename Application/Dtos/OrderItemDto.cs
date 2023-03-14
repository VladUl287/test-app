namespace Application.Dtos;

public sealed class OrderItemDto
{
    public string Name { get; set; }

    public string Unit { get; set; }

    public decimal Quantity { get; set; }

    public int OrderId { get; init; }
}