namespace Application.Dtos;

public sealed class OrdersDto
{
    public int Id { get; init; }

    public string Number { get; init; }

    public DateTime Date { get; init; }
}