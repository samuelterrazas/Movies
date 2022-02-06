using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Movies.Application;
using Movies.Infrastructure;
using Movies.WebAPI.Filters;
using Movies.WebAPI.Middlewares;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Movies.WebAPI;

public class Startup
{
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    private IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationLayer();
        services.AddInfrastructureLayer(Configuration);

        services.AddHttpContextAccessor();

        services.AddControllers(options => 
                options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation(f => f.AutomaticValidationEnabled = false);

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    
        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "Movies API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                In = OpenApiSecurityApiKeyLocation.Header,
                Name = "Authorization",
                Description = "Type into the text box: Bearer {your JWT token}"
            });
            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseOpenApi(settings => settings.Path = "api/docs/{documentName}/specification.json");
            app.UseSwaggerUi3(settings =>
            {
                settings.DocumentPath = "api/docs/{documentName}/specification.json";
                settings.Path = "/api/docs";
            });
        }
        
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        
        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();
        
        app.UseEndpoints(e => e.MapControllers());
    }
}