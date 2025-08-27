using RealEstate.Domain.Entities;

namespace RealEstate.Domain
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetFilteredAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, CancellationToken ct);

        Task<PropertyDetails> GetFilteredPropertyDetailsAsync(string? IdProperty, CancellationToken ct);
    }
}
