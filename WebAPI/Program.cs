using FluentValidation.AspNetCore;
using Movies.Application;
using Movies.Common;
using Movies.Infrastructure;
using Movies.WebAPI.Filters;
using Movies.WebAPI.Middlewares;
using NSwag;
using NSwag.Generation.Processors.Security;

// Services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
//builder.Services.AddCommonLayer();
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