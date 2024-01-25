using Microsoft.AspNetCore.Cors.Infrastructure;
using Myrtex.Application;
using Myrtex.Application.Common.Mappings;
using Myrtex.Application.Interfaces;
using Myrtex.Persistence;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;

const string CorsPolicy = "chat-policy";

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

ConfigureApp(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IMyrtexDbContext).Assembly));
    });

    services.AddApplication();
    services.AddPersistence(builder.Configuration);
    services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: CorsPolicy, policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
                ;
        });
    });
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(cfg =>
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "Myrtex.WebApi.xml");
        cfg.IncludeXmlComments(filePath);
    });
}

void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyrtexDbContext>();
    DbInitializer.Initialize(dbContext);

    app.UseCors(CorsPolicy);
    app.UseAuthorization();

    app.MapControllers();
}
