using MongoDB.Bson;
using MongoDB.Driver;
using RealEstate.Domain;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoDatabase _db;

        public PropertyRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<List<Property>> GetFilteredAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, CancellationToken ct)
        {
            var filterBuilder = Builders<Property>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrWhiteSpace(name))
                filter &= filterBuilder.Regex(p => p.Name, new BsonRegularExpression(name, "i"));

            if (!string.IsNullOrWhiteSpace(address))
                filter &= filterBuilder.Regex(p => p.Address, new BsonRegularExpression(address, "i"));

            if (minPrice.HasValue)
                filter &= filterBuilder.Gte(p => p.Price, minPrice.Value);

            if (maxPrice.HasValue)
                filter &= filterBuilder.Lte(p => p.Price, maxPrice.Value);

            try
            {
                var properties = await _db.GetCollection<Property>("Properties")
                                          .Find(filter)
                                          .ToListAsync(ct);
                var ids = properties.Select(p => p.IdProperty).ToList();

                var imageFilter = Builders<PropertyImage>.Filter.And(
                    Builders<PropertyImage>.Filter.In(img => img.IdProperty, ids),
                    Builders<PropertyImage>.Filter.Eq(img => img.Enabled, true)
                );

                var images = await _db.GetCollection<PropertyImage>("PropertyImages")
                                      .Find(imageFilter)
                                      .ToListAsync(ct);

                var imageDict = images
                    .GroupBy(i => i.IdProperty)
                    .ToDictionary(g => g.Key, g => g.First().File);

                foreach (var prop in properties)
                {
                    imageDict.TryGetValue(prop.IdProperty, out var image);
                    prop.Image = image ?? string.Empty;
                }

                return properties;
            }
            catch (MongoException ex)
            {
                throw new ApplicationException("Error al consultar MongoDB", ex);
            }
            

        }

        public async Task<PropertyDetails> GetFilteredPropertyDetailsAsync(string? idProperty, CancellationToken ct)
        {
            try
            {
                var property = await _db.GetCollection<Property>("Properties")
                   .Find(p => p.IdProperty == idProperty)
                   .FirstOrDefaultAsync(ct);

                if (property is null)
                    throw new KeyNotFoundException("Property not found");

                var owner = await _db.GetCollection<Owner>("Owners")
                    .Find(o => o.IdOwner == property.IdOwner)
                    .FirstOrDefaultAsync(ct);

                var images = _db.GetCollection<PropertyImage>("PropertyImages")
                    .Find(img => img.IdProperty == idProperty && img.Enabled)
                    .ToListAsync(ct);

                var traces = _db.GetCollection<PropertyTrace>("PropertyTraces")
                    .Find(t => t.IdProperty == idProperty)
                    .ToListAsync(ct);

                await Task.WhenAll(images, traces);

                return new PropertyDetails
                {
                    Property = property,
                    Owner = owner!,
                    Images = images.Result,
                    Traces = traces.Result
                };
            }
            catch (MongoException ex)
            {
                throw new ApplicationException("Error al consultar MongoDB", ex);
            }


        }
    }
}
