using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Common.Behaviours;
using System.Reflection;

namespace Movies.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            ValidatorOptions.Global.LanguageManager.Enabled = false;

            return services;
        }
    }
}
