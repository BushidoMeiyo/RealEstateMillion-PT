using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RealEstate.Domain.Entities;
using System.Text.Json;

namespace RealEstate.Infrastructure.Seed
{
    public class SeedOwners
    {
        private readonly IMongoDatabase _db;
        private readonly string _filePath;

        public SeedOwners(IMongoDatabase db, string fileName)
        {
            _db = db;
            _filePath = fileName;
        }

        public async Task RunAsync()
        {
            try
            {
                var collection = _db.GetCollection<Owner>("Owners");
                var count = await collection.CountDocumentsAsync(_ => true);
                if (count > 0) return;

                if (!File.Exists(_filePath))
                {
                    Console.WriteLine($"Seed file not found: {_filePath}");
                    return;
                }

                var json = await File.ReadAllTextAsync(_filePath);

                var owners = JsonSerializer.Deserialize<List<Owner>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (owners != null && owners.Any())
                {
                    await collection.InsertManyAsync(owners);
                    Console.WriteLine($"Seeded {owners.Count} documents into {collection.CollectionNamespace.CollectionName}");
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while seeding {Path.GetFileName(_filePath)}: {ex.Message}");
            }
        }
    }
}
