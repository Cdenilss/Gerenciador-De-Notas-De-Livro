using GerenciadorDeLivro.Application.Services;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Infrastructure.Auth;
using GerenciadorDeLivro.Infrastructure.Notifiication;
using GerenciadorDeLivro.Infrastructure.Persistence;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;
using GerenciadorDeLivro.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace GerenciadorDeLivro.Infrastructure;

public static class InfrasModule
{
    public static IServiceCollection AddInfrasModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository();
        services.AddUnitOfWork();
        services.AddData(configuration);
        services.AddJwtAuth(configuration);
        services.AddEmailService(configuration);
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

    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthServices, AuthService>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = TokenHelpers.GetTokenValidateParameters(configuration);
            });
        services.AddAuthorization();
        return services;

    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailServices, EmailServices>();
        var apiKey = configuration.GetValue<string>("Resend:ApiKey")
            ?? throw new InvalidOperationException("Resend:ApiKey não configurado.");
        
        services.AddResend(r =>
        {
            r.ApiToken = apiKey;
        });
        
        return services;
    }

}
