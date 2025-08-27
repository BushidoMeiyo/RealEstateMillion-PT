using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Domain;

namespace RealEstate.Application.PropertiesService.Queries;

public class GetPropertiesHandler : IRequestHandler<GetPropertiesQuery, List<PropertyDto>>
{
    private readonly IPropertyRepository _repository;

    public GetPropertiesHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PropertyDto>> Handle(GetPropertiesQuery request, CancellationToken ct)
    {
        var properties = await _repository.GetFilteredAsync(request.Name, request.Address, request.MinPrice, request.MaxPrice, ct);

        return properties.Select(p => new PropertyDto
        {
            IdProperty = p.IdProperty,
            IdOwner = p.IdOwner,
            Name = p.Name,
            Address = p.Address,
            Price = p.Price,
            CodeInternal = p.CodeInternal,
            Year = p.Year,
            Image = p.Image
        }).ToList();
    }
}
