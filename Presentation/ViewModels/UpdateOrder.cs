using Application.Dtos;

namespace Presentation.ViewModels;

public sealed class UpdateOrder
{
    public ProviderDto[] Providers { get; set; } = Array.Empty<ProviderDto>();

    public OrderDto Order { get; set; } = default!;
}