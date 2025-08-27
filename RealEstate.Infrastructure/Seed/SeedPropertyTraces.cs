using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Seed
{
    public class SeedPropertyTraces
    {
        private readonly IMongoDatabase _db;
        private readonly string _filePath;

        public SeedPropertyTraces(IMongoDatabase db, string filePath)
        {
            _db = db;
            _filePath = filePath;
        }

        public async Task RunAsync()
        {
            try
            {
                var collection = _db.GetCollection<PropertyTrace>("PropertyTraces");
                var count = await collection.CountDocumentsAsync(_ => true);
                if (count > 0) return;

                if (!File.Exists(_filePath))
                {
                    Console.WriteLine($"Seed file not found: {_filePath}");
                    return;
                }

                var json = await File.ReadAllTextAsync(_filePath);
                var propertiesTraces = JsonSerializer.Deserialize<List<PropertyTrace>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (propertiesTraces != null && propertiesTraces.Any())
                {
                    await collection.InsertManyAsync(propertiesTraces);
                    Console.WriteLine($"Seeded {propertiesTraces.Count} documents into {collection.CollectionNamespace.CollectionName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while seeding {Path.GetFileName(_filePath)}: {ex.Message}");
            }
        }
    }
}
