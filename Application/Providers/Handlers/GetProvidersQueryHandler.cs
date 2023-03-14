using Application.Dtos;
using Application.Providers.Queries;
using Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace Application.Providers.Handlers;

public sealed class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, IEnumerable<ProviderDto>>
{
    private readonly IProviderRepository providerRepository;
    private readonly IMapper mapper;

    public GetProvidersQueryHandler(IProviderRepository providerRepository, IMapper mapper)
    {
        this.providerRepository = providerRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ProviderDto>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
    {
        var providers = await providerRepository.GetProviders();

        return mapper.Map<IEnumerable<ProviderDto>>(providers);
    }
}