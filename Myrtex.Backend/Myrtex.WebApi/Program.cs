using Myrtex.Application;
using Myrtex.Application.Common.Mappings;
using Myrtex.Application.Interfaces;
using Myrtex.Persistence;
using System.Reflection;

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
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
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

    app.UseAuthorization();

    app.MapControllers();
}
