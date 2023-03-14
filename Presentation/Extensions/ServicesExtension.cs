using Application.Pipeline;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Presentation.Extensions;

public static class ServicesExtension
{
    public static void AddMediatR<TMarker>(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(TMarker).Assembly);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    public static void AddMapper<TMarker>(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(TMarker).Assembly);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}