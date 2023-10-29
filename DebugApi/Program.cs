using System.Reflection;
using Microsoft.EntityFrameworkCore;
using DebugApi.Common;
using DebugApi.Features;
using DebugApi.Infrastructure.Mapster;
using DebugApi.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options => options.CustomSchemaIds(type => type.FullName!.Replace("+", ".")))
    .AddSqlite<AppDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddMapster()
    .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
    .AddProblemDetails();

// Start of BPS
if (!builder.Environment.IsEnvironment("Test"))
{
}

builder.Services.Configure<KeyVaultOptions>(builder.Configuration.GetSection("KeyVaultOptions"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment() ||
    app.Environment.IsEnvironment("Test") ||
    app.Environment.IsProduction())
{
    await using var scope = app.Services.CreateAsyncScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await dbContext.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapControllerEndpoints();

app.Run();
