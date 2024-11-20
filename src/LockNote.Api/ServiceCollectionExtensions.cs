using LockNote.Bl;
using LockNote.Data;

namespace LockNote;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(CosmosRepository<>));
        services.AddScoped<NotesService>();

        return services;
    }
}