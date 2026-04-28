using GerenciadorDeLivro.Application.Commands.LivrosCommands;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDeLivro.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHandlers();
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertLivroCommand>());
        return services;
    }
}