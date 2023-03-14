using Domain.Entities;

namespace Domain.Repositories;

public interface IProviderRepository
{
    Task<IEnumerable<Provider>> GetProviders();
}