using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RealEstate.Application.PropertiesService.Queries;
using RealEstate.Domain;
using RealEstate.Infrastructure;
using RealEstate.Infrastructure.Seed;
using RealEstate.Infrastructure.Settings;


var builder = WebApplication.CreateBuilder(args);

const string CorsPolicy = "AllowVite";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetPropertiesQuery>();
});

builder.Services.Configure<MongoOptions>(
    builder.Configuration.GetSection("Mongo"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
    return new MongoClient(options.ConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(options.Database);
});

var seedDataPath = Path.Combine(builder.Environment.ContentRootPath, "SeedData");

builder.Services.AddScoped(sp => new SeedOwners(
    sp.GetRequiredService<IMongoDatabase>(),
    Path.Combine(seedDataPath, "seed-owner.json")
));
builder.Services.AddScoped(sp => new SeedProperties(
    sp.GetRequiredService<IMongoDatabase>(),
    Path.Combine(seedDataPath, "seed-properties.json")
));
builder.Services.AddScoped(sp => new SeedPropertyImages(
    sp.GetRequiredService<IMongoDatabase>(),
    Path.Combine(seedDataPath, "seed-images.json")
));
builder.Services.AddScoped(sp => new SeedPropertyTraces(
    sp.GetRequiredService<IMongoDatabase>(),
    Path.Combine(seedDataPath, "seed-traces.json")
));

// -----------------------------------------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    await scope.ServiceProvider.GetRequiredService<SeedOwners>().RunAsync();
    await scope.ServiceProvider.GetRequiredService<SeedProperties>().RunAsync();
    await scope.ServiceProvider.GetRequiredService<SeedPropertyImages>().RunAsync();
    await scope.ServiceProvider.GetRequiredService<SeedPropertyTraces>().RunAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(CorsPolicy);
app.UseAuthorization();
app.MapControllers();
app.Run();
