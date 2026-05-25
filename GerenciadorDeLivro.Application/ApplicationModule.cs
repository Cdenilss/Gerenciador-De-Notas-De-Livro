using FluentValidation;
using GerenciadorDeLivro.Application.Behaviors;
using GerenciadorDeLivro.Application.Commands.AvaliacaoCommands;
using GerenciadorDeLivro.Application.Commands.LivrosCommands;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Application.Models.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDeLivro.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHandlers();
        services.AddValidators();
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertLivroCommand>());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient<IPipelineBehavior<InsertLivroCommand, ResultViewModel<Guid>>, ValidateInsertLivroCommandBehavior>();
        services.AddTransient<IPipelineBehavior<InsertAvaliacaoCommand, ResultViewModel<Guid>>,ValidateInsertAvaliacaoCommandBehavior>();
        services.AddTransient<IPipelineBehavior<InsertUsuarioCommand, ResultViewModel<Guid>>,ValidateInsertUsuarioCommandBehavior>();
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationModule).Assembly);
        return services;
    }
}
