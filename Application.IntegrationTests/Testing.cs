using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Movies.Infrastructure.Identity;
using Movies.Infrastructure.Persistence;
using Movies.WebAPI;
using NUnit.Framework;
using Respawn;

namespace Movies.Application.IntegrationTests;

[SetUpFixture]
public class Testing
{
    private static IConfigurationRoot _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static Checkpoint _checkpoint = null!;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();

        var startup = new Startup(_configuration);

        var services = new ServiceCollection();

        services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.EnvironmentName == "Development" && w.ApplicationName == "Movies.WebAPI"));

        services.AddLogging();
        
        startup.ConfigureServices(services);

        _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

        _checkpoint = new Checkpoint {TablesToIgnore = new[] {"__EFMigrationsHistory"}};
        
        EnsureDatabase();
    }

    private static void EnsureDatabase()
    {
        using var scope = _scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task RunAsAdministratorAsync() =>
        await RunAsUserAsync("administrator@localhost", "Abc123.", new[] {"Administrator"});

    private static async Task RunAsUserAsync(string email, string password, string[] roles)
    {
        using var scope = _scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = new ApplicationUser {UserName = email, Email = email};

        var result = await userManager.CreateAsync(user, password);

        if (roles.Any())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
                await roleManager.CreateAsync(new IdentityRole(role));

            await userManager.AddToRolesAsync(user, roles);
        }

        var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

        throw new Exception($"Unable to create {email}.{Environment.NewLine}{errors}");
    }

    public static async Task ResetState() =>
        await _checkpoint.Reset(_configuration.GetConnectionString("SQLServerConnection"));

    public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await dbContext.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
    {
        using var scope = _scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Add(entity);

        await dbContext.SaveChangesAsync();
    }
}