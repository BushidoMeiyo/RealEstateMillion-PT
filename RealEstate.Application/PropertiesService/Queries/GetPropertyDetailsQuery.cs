using MediatR;
using RealEstate.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.PropertiesService.Queries
{
    public record GetPropertyDetailsQuery(
        string? IdProperty
    ) : IRequest<PropertyDetailsDto>;
}
