namespace Movies.WebAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

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

        services.AddHealthChecks()
            .AddSqlServer(
                connectionString: Configuration.GetConnectionString("SQLServerConnection"),
                name: "SQLServer",
                tags: new[] {"HealthCheck"},
                timeout: TimeSpan.FromSeconds(10)
            );
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
        
        app.UseEndpoints(e =>
        {
            e.MapControllers();

            e.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("HealthCheck"),
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonSerializer.Serialize(report.Entries.Select(entry => new
                    {
                        Name = entry.Key,
                        Status = Convert.ToString(entry.Value.Status),
                        Exception = entry.Value.Exception is not null ? entry.Value.Exception.Message : "None",
                        Duration = entry.Value.Duration
                    }));

                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });
        });
    }
}