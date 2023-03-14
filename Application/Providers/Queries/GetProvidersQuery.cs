using Application.Dtos;
using MediatR;

namespace Application.Providers.Queries;

public sealed class GetProvidersQuery : IRequest<IEnumerable<ProviderDto>>
{
}