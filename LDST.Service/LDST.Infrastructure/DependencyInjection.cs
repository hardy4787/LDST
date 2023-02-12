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
using LDST.Application.Interfaces.Persistance;
using LDST.Application.Interfaces.Services;
using LDST.Application.Interfaces;
using LDST.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using LDST.Domain.EFModels;

namespace LDST.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDatabases(configuration);
        services.AddAuth(configuration);
        services.AddFileManager(configuration);
        services.AddEmailConfiguration(configuration);
        return services;
    }

    private static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var emailConfig = configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
        services.AddSingleton(emailConfig!);
        services.AddScoped<IEmailSender, EmailSender>();
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

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
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

        services.AddIdentity<UserEntity, IdentityRole>((options) =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
            options.User.RequireUniqueEmail = true;

            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            options.Lockout.MaxFailedAccessAttempts = 3;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(2));

        return services;
    }
}
