using System.Text;
using LDST.Infrastructure.Authentication;
using LDST.Infrastructure.Persistance;
using LDST.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using LDST.Infrastructure.Persistance.Repositories;
using LDST.Application.Interfaces.Persistance;
using LDST.Application.Interfaces.Services;
using LDST.Application.Interfaces;
using LDST.Infrastructure.Models;

namespace LDST.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddDatabases(configuration);
        services.AddFileManager(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    private static void AddFileManager(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobStorageOptions>(options => configuration.GetSection("BlobStorageOptions").Bind(options));
        services.AddScoped<IContainerNameResolver, ContainerNameResolver>();
        services.AddScoped<IFileManager, FileManager>();
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)
                    ),
                });

        return services;
    }

    private static IServiceCollection AddDatabases(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>()!);

        string? connectionString = configuration.GetConnectionString("LDSTConnection");

        services.AddDbContext<AppDbContext>(
            (options) =>
            {
                options.UseNpgsql(connectionString);
            }
        );

        return services;
    }
}
