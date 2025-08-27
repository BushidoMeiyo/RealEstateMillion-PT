using RealEstate.Application.DTOs;
using RealEstate.Domain;
using MediatR;


namespace RealEstate.Application.PropertiesService.Queries;

public class GetPropertyDetailsHandler : IRequestHandler<GetPropertyDetailsQuery, PropertyDetailsDto>
{
    private readonly IPropertyRepository _repository;

    public GetPropertyDetailsHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<PropertyDetailsDto> Handle(GetPropertyDetailsQuery request, CancellationToken ct)
    {
        var result = await _repository.GetFilteredPropertyDetailsAsync(request.IdProperty, ct);

        if (result == null)
            throw new ArgumentNullException("No details were found for the property with the provided ID.");

        return new PropertyDetailsDto
        {
            Property = new PropertyDto
            {
                IdProperty = result.Property.IdProperty,
                Name = result.Property.Name,
                Address = result.Property.Address,
                Price = result.Property.Price,
                CodeInternal = result.Property.CodeInternal,
                Year = result.Property.Year,
                IdOwner = result.Property.IdOwner,
                Image = result.Property.Image
            },
            Owner = new OwnerDto
            {
                IdOwner = result.Owner.IdOwner,
                Name = result.Owner.Name,
                Address = result.Owner.Address,
                Birthday = result.Owner.Birthday,
                Photo = result.Owner.Photo
            },
            Images = [.. result.Images.Select(img => new PropertyImageDto
            {
                IdPropertyImage = img.IdPropertyImage,
                IdProperty = img.IdProperty,
                File = img.File,
                Enabled = img.Enabled
            })],
            Traces = [.. result.Traces.Select(trace => new PropertyTraceDto
            {
                IdPropertyTrace = trace.IdPropertyTrace,
                IdProperty = trace.IdProperty,
                Name = trace.Name,
                DateSale = trace.DateSale,
                Value = trace.Value,
                Tax = trace.Tax
            })]
        };
    }
}

