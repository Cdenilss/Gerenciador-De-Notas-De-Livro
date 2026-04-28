using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;
using GerenciadorDeLivro.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDeLivro.Infrastructure;

public static class InfrasModule
{
    public static IServiceCollection AddInfrasModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository();
        services.AddData(configuration);
        return services;
        
    }
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GerenciadorDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<ILivroRepository, LivroRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
        return services;
    }

}