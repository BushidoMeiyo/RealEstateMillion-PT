using MongoDB.Driver;
using RealEstate.Domain.Entities;
using System.Text.Json;
namespace RealEstate.Infrastructure.Seed
{
    public class SeedProperties
    {
        private readonly IMongoDatabase _db;
        private readonly string _filePath;

        public SeedProperties(IMongoDatabase db, string filePath)
        {
            _db = db;
            _filePath = filePath;
        }

        public async Task RunAsync()
        {
            try
            {
                var collection = _db.GetCollection<Property>("Properties");
                var count = await collection.CountDocumentsAsync(_ => true);
                if (count > 0) return;

                if (!File.Exists(_filePath))
                {
                    Console.WriteLine($"Seed file not found: {_filePath}");
                    return;
                }

                var json = await File.ReadAllTextAsync(_filePath);

                var properties = JsonSerializer.Deserialize<List<Property>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (properties != null && properties.Any())
                {
                    await collection.InsertManyAsync(properties);
                    Console.WriteLine($"Seeded {properties.Count} documents into {collection.CollectionNamespace.CollectionName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while seeding {Path.GetFileName(_filePath)}: {ex.Message}");
            }
        }
    }
}
