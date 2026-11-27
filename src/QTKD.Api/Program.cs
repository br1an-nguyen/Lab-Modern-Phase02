using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using QTKD.Api.Data;
using QTKD.Api.Services;

var seedRequested = args.Contains("--seed", StringComparer.Ordinal);
var builder = WebApplication.CreateBuilder(args.Where(arg => arg != "--seed").ToArray());

var connectionString = builder.Configuration.GetConnectionString("Qtkd");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Configure ConnectionStrings:Qtkd with User Secrets (Development) " +
        "or the ConnectionStrings__Qtkd environment variable before running the API or EF tools.");
}

builder.Services.AddDbContext<QtkdDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<DevelopmentSeeder>();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

if (seedRequested)
{
    if (!app.Environment.IsDevelopment())
    {
        throw new InvalidOperationException("The demo seeder can only run in Development.");
    }

    await using var scope = app.Services.CreateAsyncScope();
    await scope.ServiceProvider.GetRequiredService<DevelopmentSeeder>().SeedAsync();
    app.Logger.LogInformation("Demo seed completed. Existing rows were preserved.");
    return;
}

app.UseExceptionHandler();
app.UseStatusCodePages();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();
