using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.PropertiesService.Queries
{
    public record GetPropertiesQuery(
    string? Name,
    string? Address,
    decimal? MinPrice,
    decimal? MaxPrice
    ) : IRequest<List<PropertyDto>>;
}
