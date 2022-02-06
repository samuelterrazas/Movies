using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Movies.Infrastructure.Identity;
using Movies.Infrastructure.Persistence;
using System.Text;
using Movies.Common.Interfaces;
using Movies.Infrastructure.Azure;

namespace Movies.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("SQLServerConnection"),
                migration => migration.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
        );

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        
        #region Authentication
        services.Configure<Jwt>(configuration.GetSection("Jwt"));
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    RoleClaimType = ClaimTypes.Role
                };
            });

        services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<ITokenHandlerService, TokenHandlerService>();
        services.AddScoped<IIdentityService, IdentityService>();
        #endregion

        services.AddTransient<IFileStore, AzureFileStore>();

        return services;
    }
}
