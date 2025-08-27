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
    public class SeedPropertyImages
    {
        private readonly IMongoDatabase _db;
        private readonly string _filePath;

        public SeedPropertyImages(IMongoDatabase db, string filePath)
        {
            _db = db;
            _filePath = filePath;
        }

        public async Task RunAsync()
        {
            try
            {
                var collection = _db.GetCollection<PropertyImage>("PropertyImages");
                var count = await collection.CountDocumentsAsync(_ => true);
                if (count > 0) return;

                if (!File.Exists(_filePath))
                {
                    Console.WriteLine($"Seed file not found: {_filePath}");
                    return;
                }

                var json = await File.ReadAllTextAsync(_filePath);
                var propertiesImage = JsonSerializer.Deserialize<List<PropertyImage>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (propertiesImage != null && propertiesImage.Any())
                {
                    await collection.InsertManyAsync(propertiesImage);
                    Console.WriteLine($"Seeded {propertiesImage.Count} documents into {collection.CollectionNamespace.CollectionName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while seeding {Path.GetFileName(_filePath)}: {ex.Message}");
            }
        }
    }
}
