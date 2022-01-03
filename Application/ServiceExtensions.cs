using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Movies.Application.Common.Behaviours;

namespace Movies.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        ValidatorOptions.Global.LanguageManager.Enabled = false;

        return services;
    }
}
