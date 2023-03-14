using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class ProviderRepsoitory : IProviderRepository
{
    private readonly DatabaseContext databaseContext;

    public ProviderRepsoitory(DatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public async Task<IEnumerable<Provider>> GetProviders()
    {
        return await databaseContext.Providers
            .AsNoTracking()
            .ToListAsync();
    }
}