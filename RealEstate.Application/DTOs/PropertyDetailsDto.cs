using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.DTOs
{
    public class PropertyDetailsDto  
    {
        public required PropertyDto Property { get; set; }   
        public required OwnerDto Owner { get; set; }
        public required List<PropertyImageDto>Images { get; set; }
        public required List<PropertyTraceDto>Traces { get; set; }
    }
}
