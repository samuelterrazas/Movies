using FluentValidation.AspNetCore;
using Movies.Application;
using Movies.Infrastructure;
using Movies.Infrastructure.Persistence;
using Movies.WebAPI.Filters;
using Movies.WebAPI.Middlewares;
using NSwag;
using NSwag.Generation.Processors.Security;

// Services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options => 
        options.Filters.Add<ApiExceptionFilterAttribute>())
    .AddFluentValidation(f => f.AutomaticValidationEnabled = false);
    
builder.Services.AddOpenApiDocument(configure =>
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

// App
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        await ApplicationDbContextSeed.SeedSampleDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database");
        throw;
    }
}

if (app.Environment.IsDevelopment())
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();