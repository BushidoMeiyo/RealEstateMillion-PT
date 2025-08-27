using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
using RealEstate.Domain;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RealEstate.Infrastructure.Tests.Integration
{
    [TestFixture]
    public class PropertyRepositoryIntegrationTests
    {
        private MongoDbRunner _runner;
        private IMongoDatabase _database;
        private IPropertyRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _runner = MongoDbRunner.Start();
            var client = new MongoClient(_runner.ConnectionString);
            _database = client.GetDatabase("IntegrationTestDB");
            _repository = new PropertyRepository(_database);
        }

        [TearDown]
        public void TearDown()
        {
            _runner.Dispose();
        }

        [Test]
        public async Task GetFilteredAsync_ShouldReturnPropertiesWithFirstEnabledImage()
        {
            var property1 = new Property { IdProperty = "prop1", Name = "Casa Bella", Address = "Calle 123", Price = 100000, IdOwner = "owner1" };
            var property2 = new Property { IdProperty = "prop2", Name = "Casa Bonita", Address = "Avenida 456", Price = 200000, IdOwner = "owner2" };

            try
            {
                await _database.GetCollection<Property>("Properties").InsertManyAsync(new[] { property1, property2 });
            }
            catch (MongoBulkWriteException ex)
            {
                foreach (var error in ex.WriteErrors)
                {
                    Console.WriteLine($"Error: {error.Message}");
                }
                throw;
            }

            var image1 = new PropertyImage { IdPropertyImage = "image1", IdProperty = "prop1", File = "foto1.jpg", Enabled = true };
            var image2 = new PropertyImage { IdPropertyImage = "image2", IdProperty = "prop2", File = "foto2.jpg", Enabled = true };
            var image3 = new PropertyImage { IdPropertyImage = "image3", IdProperty = "prop2", File = "foto2b.jpg", Enabled = true };

            try
            {
                await _database.GetCollection<PropertyImage>("PropertyImages").InsertManyAsync(new[] { image1, image2, image3 });
            }
            catch (MongoBulkWriteException ex)
            {
                foreach (var error in ex.WriteErrors)
                {
                    Console.WriteLine($"Error: {error.Message}");
                }
                throw;
            }

            var result = await _repository.GetFilteredAsync(null, null, null, null, default);

            Assert.That(result, Has.Count.EqualTo(2));
            var prop1 = result.FirstOrDefault(p => p.IdProperty == "prop1");
            var prop2 = result.FirstOrDefault(p => p.IdProperty == "prop2");

            Assert.IsNotNull(prop1);
            Assert.IsNotNull(prop2);
            Assert.That(prop1.Image, Is.EqualTo("foto1.jpg"));
            Assert.That(prop2.Image, Is.EqualTo("foto2.jpg"));
        }

        [Test]
        public async Task GetFilteredPropertyDetailsAsync_ShouldReturnDetailsWithOwnerImagesAndTraces()
        {
            try
            {
                var property = new Property { IdProperty = "propX", Name = "Casa X", Address = "Dirección X", Price = 300000, IdOwner = "ownerX" };
                await _database.GetCollection<Property>("Properties").InsertOneAsync(property);
                
                var owner = new Owner { IdOwner = "ownerX", Name = "Juan Pérez", Address = "Calle 18", Photo = "photo.png", Birthday = "1995-01--02" };
                await _database.GetCollection<Owner>("Owners").InsertOneAsync(owner);

                var image1 = new PropertyImage { IdPropertyImage = "image1", IdProperty = "propX", File = "img1.png", Enabled = true };
                var image2 = new PropertyImage { IdPropertyImage = "image2", IdProperty = "propX", File = "img2.png", Enabled = false };
                await _database.GetCollection<PropertyImage>("PropertyImages").InsertManyAsync(new[] { image1, image2 });

                var trace1 = new PropertyTrace { IdPropertyTrace = "trace2", IdProperty = "propX", Value = 50000, Tax = 15 };
                var trace2 = new PropertyTrace { IdPropertyTrace = "trace1", IdProperty = "propX", Value = 40000, Tax = 14 };
                await _database.GetCollection<PropertyTrace>("PropertyTraces").InsertManyAsync(new[] { trace1, trace2 });
            }
            catch (MongoBulkWriteException ex)
            {
                foreach (var error in ex.WriteErrors)
                {
                    Console.WriteLine($"Error: {error.Message}");
                }
                throw;
            }

            var result = await _repository.GetFilteredPropertyDetailsAsync("propX", default);

            Assert.IsNotNull(result);
            Assert.That(result.Property.IdProperty, Is.EqualTo("propX"));
            Assert.That(result.Owner.IdOwner, Is.EqualTo("ownerX"));
            Assert.That(result.Images, Has.Count.EqualTo(1));
            Assert.That(result.Images[0].File, Is.EqualTo("img1.png"));
            Assert.That(result.Traces, Has.Count.EqualTo(2));
        }
    }
}