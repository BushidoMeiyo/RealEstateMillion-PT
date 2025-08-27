using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.PropertiesService.Queries;

namespace RealEstate.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PropertiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetFilteredProperties")]
    public async Task<IActionResult> Get(
        [FromQuery] string? name,
        [FromQuery] string? address,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice)
    {
        var result = await _mediator.Send(new GetPropertiesQuery(name, address, minPrice, maxPrice));
        return Ok(result);
    }

    [HttpGet("{idProperty}", Name = "GetPropertyDetails")]
    public async Task<IActionResult> GetById(string idProperty)
    {
        var result = await _mediator.Send(new GetPropertyDetailsQuery(idProperty));
        return Ok(result);
    }
}
